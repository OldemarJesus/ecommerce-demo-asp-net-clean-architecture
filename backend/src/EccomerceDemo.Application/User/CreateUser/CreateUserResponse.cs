using EccomerceDemo.Domain.Entities.User.ValueObjects;

namespace EccomerceDemo.Application.User.CreateUser;

public record class CreateUserResponse(UserId Id, string Email, string FullName);
