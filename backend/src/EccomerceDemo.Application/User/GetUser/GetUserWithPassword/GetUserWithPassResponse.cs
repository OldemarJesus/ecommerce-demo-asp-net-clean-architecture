using EccomerceDemo.Domain.Entities.User.ValueObjects;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithPassword;

public record class GetUserWithPassResponse(UserId UserId, string Email, string Name, string Token);
