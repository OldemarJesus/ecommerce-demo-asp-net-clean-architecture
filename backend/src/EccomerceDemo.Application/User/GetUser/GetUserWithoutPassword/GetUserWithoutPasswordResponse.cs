using EccomerceDemo.Domain.Entities.User.ValueObjects;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithoutPassword;

public record class GetUserWithoutPasswordResponse(UserId UserId, string Email, string FullName);