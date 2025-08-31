
using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.Common.Interfaces.Authentication;
using EccomerceDemo.Application.User.Interfaces;
using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithPassword;

public class GetUserWithPassQueryHandler : IQueryHandler<GetUserWithPassQuery, GetUserWithPassResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public GetUserWithPassQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<Result<GetUserWithPassResponse>> Handle(GetUserWithPassQuery query, CancellationToken cancellationToken)
    {
        // check if user exists
        var user = await _userRepository.GetByEmailAsync(query.Email, cancellationToken);
        if (user is null)
        {
            return Result.Failure<GetUserWithPassResponse>(DomainError.Generic.NotFound);
        }

        // verify password
        var isPasswordValid = _passwordHasher.Verify(query.Password, user.Password);
        if (!isPasswordValid)
        {
            return Result.Failure<GetUserWithPassResponse>(DomainError.Generic.User.InvalidCredentials);
        }

        // generate token
        var token = _tokenGenerator.GenerateToken(user.Id.Value, user.FullName, user.Email);

        // map to response
        return Result.Success(new GetUserWithPassResponse(user.Id, user.Email, user.FullName, token));
    }
}
