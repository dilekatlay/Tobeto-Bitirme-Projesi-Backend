using FluentValidation;

namespace Application.Features.Managements.Commands.Update;

public class UpdateManagementCommandValidator : AbstractValidator<UpdateManagementCommand>
{
    public UpdateManagementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.InventoryId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}