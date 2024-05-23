using FluentValidation;

namespace Application.Features.InventoryManagements.Commands.Delete;

public class DeleteInventoryManagementCommandValidator : AbstractValidator<DeleteInventoryManagementCommand>
{
    public DeleteInventoryManagementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}