using FluentValidation;

namespace Application.Features.CreditCarts.Commands.Delete;

public class DeleteCreditCartCommandValidator : AbstractValidator<DeleteCreditCartCommand>
{
    public DeleteCreditCartCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}