using FluentValidation;

namespace EccomerceDemo.Application.Product.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product description is required.");

        RuleFor(x => x.PriceInCents.AmountInCents)
            .NotNull()
            .WithMessage("Product price is required.")
            .GreaterThan(0)
            .WithMessage("Product price must be positive.");
        
        RuleFor(x => x.PriceInCents.Currency)
            .NotNull()
            .WithMessage("Product currency is required.");

        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("User ID is required.");
    }
}
