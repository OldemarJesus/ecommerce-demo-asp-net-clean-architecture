using FluentValidation;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithPassword;

public class GetUserWithPassQueryValidator : AbstractValidator<GetUserWithPassQuery>
{
    public GetUserWithPassQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
