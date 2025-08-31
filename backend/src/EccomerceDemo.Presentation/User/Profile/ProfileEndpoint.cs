using System;
using System.Security.Claims;

using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.User.GetUser.GetUserWithoutPassword;
using EccomerceDemo.Domain.Entities.User.ValueObjects;

using FastEndpoints;

using Microsoft.AspNetCore.Http;

namespace EccomerceDemo.Presentation.User.Profile;

public class ProfileEndpoint : Endpoint<EmptyRequest, ProfileResponse>
{
    private readonly IQueryHandler<GetUserWithoutPasswordQuery, GetUserWithoutPasswordResponse> _queryHandler;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProfileEndpoint(IQueryHandler<GetUserWithoutPasswordQuery, GetUserWithoutPasswordResponse> queryHandler, IHttpContextAccessor httpContextAccessor)
    {
        _queryHandler = queryHandler;
        _httpContextAccessor = httpContextAccessor;
    }

    public override void Configure()
    {
        Get("user/profile");
        Description(b =>
            b.WithTags("User")
            .Produces<ProfileResponse>(StatusCodes.Status200OK)
            .WithDescription("Get user profile")
            .ProducesProblem(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        // extract ID from jwt headers
        var userId = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var guId = Guid.TryParse(userId, out var parsedUserId) ? parsedUserId : Guid.Empty;
        var query = new GetUserWithoutPasswordQuery(UserId.Create(guId));
        var result = await _queryHandler.Handle(query, ct);
        if (result.IsFailure)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(new ProfileResponse(result.Value.UserId.Value, result.Value.Email, result.Value.FullName), ct);
    }
}
