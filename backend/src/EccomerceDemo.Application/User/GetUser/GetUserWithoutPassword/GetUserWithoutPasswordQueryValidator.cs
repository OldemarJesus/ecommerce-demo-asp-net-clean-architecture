using FluentValidation;

namespace EccomerceDemo.Application.User.GetUser.GetUserWithoutPassword;

public class GetUserWithoutPasswordQueryValidator : AbstractValidator<GetUserWithoutPasswordQuery>
{
    public GetUserWithoutPasswordQueryValidator()
    {
        RuleFor(x => x.UserId.Value).NotEmpty();
    }
}
