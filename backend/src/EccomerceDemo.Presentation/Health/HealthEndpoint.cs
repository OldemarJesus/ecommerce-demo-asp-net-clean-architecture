using FastEndpoints;

using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.Health;

public class HealthEndpoint : Endpoint<EmptyRequest, HealthResponse>
{
    public override void Configure()
    {
        Get("/health");
        AllowAnonymous();
        Description(b => b.WithTags("Health").Produces<HealthResponse>(StatusCodes.Status200OK));
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        await SendAsync(new HealthResponse { Status = "Healthy" }, cancellation: ct);
    }
}
