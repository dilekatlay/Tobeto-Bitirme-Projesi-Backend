using FluentValidation;

namespace Application.Features.CargoTrackings.Commands.Delete;

public class DeleteCargoTrackingCommandValidator : AbstractValidator<DeleteCargoTrackingCommand>
{
    public DeleteCargoTrackingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}