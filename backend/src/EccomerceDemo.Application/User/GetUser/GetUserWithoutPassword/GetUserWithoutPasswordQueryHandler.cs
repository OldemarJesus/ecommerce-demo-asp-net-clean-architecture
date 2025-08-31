
using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.User.Interfaces;
using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithoutPassword;

public class GetUserWithoutPasswordQueryHandler : IQueryHandler<GetUserWithoutPasswordQuery, GetUserWithoutPasswordResponse>
{
    private readonly IUserRepository _userRepository;

    public GetUserWithoutPasswordQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<GetUserWithoutPasswordResponse>> Handle(GetUserWithoutPasswordQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);
        if (user is null)
        {
            return Result.Failure<GetUserWithoutPasswordResponse>(DomainError.Generic.NotFound);
        }

        var response = new GetUserWithoutPasswordResponse(user.Id, user.Email, user.FullName);
        return Result.Success(response);
    }
}
