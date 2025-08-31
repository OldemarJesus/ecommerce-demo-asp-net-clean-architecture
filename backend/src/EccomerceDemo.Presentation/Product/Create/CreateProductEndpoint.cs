using System.Security.Claims;

using EccomerceDemo.Application.Product.CreateProduct;
using EccomerceDemo.Domain.Entities.User.ValueObjects;
using EccomerceDemo.Domain.Primitives.ValueObjects;

using FastEndpoints;

using FluentValidation.Results;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.Product.Create;

public class CreateProductEndpoint : Endpoint<CreateProductRequest, ProductResponse>
{
    private readonly Application.Abstrations.Messaging.ICommandHandler<CreateProductCommand, CreateProductResponse> _commandHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateProductEndpoint(Application.Abstrations.Messaging.ICommandHandler<CreateProductCommand, CreateProductResponse> commandHandler, IHttpContextAccessor httpContextAccessor)
    {
        _commandHandler = commandHandler;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Post("/products");
        Description(b => b
            .Produces<ProductResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithName("CreateProduct")
            .WithTags("Products"));
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var priceInCentsTruncated = (long)(req.Price * 100);
        var priceInCents = Price.Create(priceInCentsTruncated, req.Currency);

        var command = new CreateProductCommand(
            Name: req.Name,
            Description: req.Description,
            PriceInCents: priceInCents,
            UserId: UserId.Create(Guid.Parse(userId!))
        );

        var result = await _commandHandler.Handle(command, ct);

        // return response
        if (result.IsSuccess)
        {
            var response = new ProductResponse(
                result.Value.ProductId.Value,
                result.Value.CreatedBy.Value,
                result.Value.Name,
                result.Value.Description,
                result.Value.PriceInCents.AmountInCents / 100.0m,
                result.Value.PriceInCents.Currency
            );
            await SendCreatedAtAsync<CreateProductEndpoint>(new { id = response.ProductId }, response, cancellation: ct);
        }
        else
        {
            // map Error to ValidationFailure
            foreach (var error in result.Errors)
            {
                AddError(new ValidationFailure(error.Code, error.Description));
            }
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
