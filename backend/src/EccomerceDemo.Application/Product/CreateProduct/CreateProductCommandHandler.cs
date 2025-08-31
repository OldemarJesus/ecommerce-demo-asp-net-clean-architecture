using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.Product.Interfaces;
using EccomerceDemo.Domain.Abstractions;

using FluentValidation;

namespace EccomerceDemo.Application.Product.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IValidator<CreateProductCommand> _validator;

    public CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
    {
        _productRepository = productRepository;
        _validator = validator;
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // validate
        var validationResult = _validator.Validate(command);
        if (!validationResult.IsValid)
        {
            return Result.Failure<CreateProductResponse>(validationResult.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage)));
        }


        // create product
        var product = Domain.Entities.Product.Product.Create(
            command.Name,
            command.Description,
            command.PriceInCents,
            command.UserId
        );

        await _productRepository.AddAsync(product);

        // map to response
        var response = new CreateProductResponse(
            product.Id,
            product.PriceInCents,
            command.UserId,
            product.Name,
            product.Description);
        return Result.Success(response);
    }
}
