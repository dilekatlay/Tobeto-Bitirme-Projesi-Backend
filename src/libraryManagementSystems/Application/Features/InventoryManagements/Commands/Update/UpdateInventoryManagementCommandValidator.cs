using FluentValidation;

namespace Application.Features.InventoryManagements.Commands.Update;

public class UpdateInventoryManagementCommandValidator : AbstractValidator<UpdateInventoryManagementCommand>
{
    public UpdateInventoryManagementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.ShelfId).NotEmpty();
        RuleFor(c => c.CategoryId).NotEmpty();
    }
}