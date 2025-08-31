using EccomerceDemo.Application.User.CreateUser;

using FastEndpoints;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.User.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly Application.Abstrations.Messaging.ICommandHandler<CreateUserCommand, CreateUserResponse> _commandHandler;

    public RegisterEndpoint(Application.Abstrations.Messaging.ICommandHandler<CreateUserCommand, CreateUserResponse> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
        Description(b =>
            b.WithTags("User")
            .Produces<RegisterResponse>(StatusCodes.Status201Created)
            .WithDescription("User registration")
            .ProducesValidationProblem(StatusCodes.Status400BadRequest));
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        // map to an command
        var command = new CreateUserCommand(
            req.Email,
            req.FullName,
            req.Password
        );

        // dispatch
        Domain.Abstractions.Result<CreateUserResponse> result = await _commandHandler.Handle(command, ct);

        // return response
        if (result.IsSuccess)
        {
            var response = new RegisterResponse(result.Value.Id.Value, result.Value.Email, result.Value.FullName);
            await SendCreatedAtAsync<RegisterEndpoint>(new { id = response.UserId }, response, cancellation: ct);
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
