using FluentValidation;

namespace Application.Features.CreditCarts.Commands.Update;

public class UpdateCreditCartCommandValidator : AbstractValidator<UpdateCreditCartCommand>
{
    public UpdateCreditCartCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.NameOnTheCart).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.CartNo).NotEmpty().Length(16);
        RuleFor(c => c.ExpirationDate).NotEmpty().Must(date => date >= DateTime.Today);
        RuleFor(c => c.Cvv).NotEmpty().Must(cvv => cvv >= 100 && cvv <= 999);
    }
}