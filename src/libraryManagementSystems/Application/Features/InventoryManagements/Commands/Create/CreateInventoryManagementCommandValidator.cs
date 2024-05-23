using FluentValidation;

namespace Application.Features.InventoryManagements.Commands.Create;

public class CreateInventoryManagementCommandValidator : AbstractValidator<CreateInventoryManagementCommand>
{
    public CreateInventoryManagementCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.ShelfId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}