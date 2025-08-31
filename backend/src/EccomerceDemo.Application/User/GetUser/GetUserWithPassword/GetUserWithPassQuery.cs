using EccomerceDemo.Application.Abstrations.Messaging;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithPassword;

public record class GetUserWithPassQuery(string Email, string Password, bool WithToken = true) : IQuery<GetUserWithPassResponse>;