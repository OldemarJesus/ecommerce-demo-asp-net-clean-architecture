using FastEndpoints;

using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.User.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, RegisterResponse>
{
    public override void Configure()
    {
        Post("/register");
        AllowAnonymous();
        Description(b =>
            b.WithTags("User")
            .Produces<RegisterResponse>(StatusCodes.Status201Created)
            .WithDescription("User registration"));
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        // Handle registration logic here
        await SendAsync(new RegisterResponse { UserId = new UserId(Guid.NewGuid().ToString()), Email = "example@example.com", FullName = "Example User" }, cancellation: ct);
    }
}
