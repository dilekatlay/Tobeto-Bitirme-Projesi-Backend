using Domain.Entities;
using FluentValidation;
using Microsoft.VisualBasic;

namespace Application.Features.CreditCarts.Commands.Create;

public class CreateCreditCartCommandValidator : AbstractValidator<CreateCreditCartCommand>
{
    public CreateCreditCartCommandValidator()
    {
        RuleFor(c => c.NameOnTheCart).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.CartNo).NotEmpty().Length(16);
        RuleFor(c => c.ExpirationDate).NotEmpty().Must(date => date >= DateTime.Today);
        RuleFor(c => c.Cvv).NotEmpty().Must(cvv => cvv >= 100 && cvv <= 999);
    }
}