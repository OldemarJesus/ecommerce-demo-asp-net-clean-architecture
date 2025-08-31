using System;

using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Domain.Entities.User.ValueObjects;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithoutPassword;

public record class GetUserWithoutPasswordQuery(UserId UserId) : IQuery<GetUserWithoutPasswordResponse>;
