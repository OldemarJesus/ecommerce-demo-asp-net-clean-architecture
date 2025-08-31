using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.User.GetUser.GetUserWithPassword;

using FastEndpoints;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.User.Login;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IQueryHandler<GetUserWithPassQuery, GetUserWithPassResponse> _queryHandler;

    public LoginEndpoint(IQueryHandler<GetUserWithPassQuery, GetUserWithPassResponse> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public override void Configure()
    {
        Post("/login");
        AllowAnonymous();
        Description(b =>
            b.WithTags("User")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .WithDescription("User login")
            .ProducesValidationProblem(StatusCodes.Status400BadRequest));
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        // map to a command
        var command = new GetUserWithPassQuery(req.Email, req.Password);

        // dispatch
        Domain.Abstractions.Result<GetUserWithPassResponse> result = await _queryHandler.Handle(command, ct);

        // return response
        if (result.IsSuccess)
        {
            var response = new LoginResponse(result.Value.UserId.Value, result.Value.Email, result.Value.Name, result.Value.Token);
            await SendAsync(response, cancellation: ct);
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
