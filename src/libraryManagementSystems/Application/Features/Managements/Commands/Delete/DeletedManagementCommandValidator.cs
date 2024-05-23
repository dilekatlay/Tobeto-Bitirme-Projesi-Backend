using FluentValidation;

namespace Application.Features.Managements.Commands.Delete;

public class DeleteManagementCommandValidator : AbstractValidator<DeleteManagementCommand>
{
    public DeleteManagementCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}