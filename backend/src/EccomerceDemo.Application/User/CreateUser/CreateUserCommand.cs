using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Domain.Abstractions;

namespace EccomerceDemo.Application.User.CreateUser;

public record class CreateUserCommand(
    string Email,
    string FullName,
    string RawPassword) : ICommand<CreateUserResponse>;
