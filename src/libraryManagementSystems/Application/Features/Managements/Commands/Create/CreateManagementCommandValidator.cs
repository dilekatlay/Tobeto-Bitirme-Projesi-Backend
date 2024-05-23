using FluentValidation;

namespace Application.Features.Managements.Commands.Create;

public class CreateManagementCommandValidator : AbstractValidator<CreateManagementCommand>
{
    public CreateManagementCommandValidator()
    {
        RuleFor(c => c.InventoryId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}