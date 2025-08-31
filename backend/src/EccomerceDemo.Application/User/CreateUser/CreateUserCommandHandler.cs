

using EccomerceDemo.Application.Abstrations.Messaging;
using EccomerceDemo.Application.Common.Interfaces.Authentication;
using EccomerceDemo.Application.User.Interfaces;
using EccomerceDemo.Domain.Abstractions;
using EccomerceDemo.Domain.Exceptions.Errors;

using FluentValidation;

namespace EccomerceDemo.Application.User.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IValidator<CreateUserCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _validator = validator;
    }

    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        // validate command
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result.Failure<CreateUserResponse>(validationResult.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage)));
        }

        // garantee no duplicate email
        var isExistingUser = await _userRepository.GetExistsByEmailAsync(command.Email, cancellationToken);
        if (isExistingUser)
        {
            return Result.Failure<CreateUserResponse>(DomainError.Generic.User.DuplicateEmail);
        }

        // hash password
        var hashedPassword = _passwordHasher.Hash(command.RawPassword);

        // create user
        var newUser = Domain.Entities.User.User.Create(command.FullName, command.Email, hashedPassword);
        await _userRepository.AddAsync(newUser, cancellationToken);

        // map to response
        var response = new CreateUserResponse(newUser.Id, newUser.Email, newUser.FullName);
        return Result.Success(response);
    }
}
