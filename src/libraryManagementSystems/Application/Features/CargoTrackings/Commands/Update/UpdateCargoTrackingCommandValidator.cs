using FluentValidation;

namespace Application.Features.CargoTrackings.Commands.Update;

public class UpdateCargoTrackingCommandValidator : AbstractValidator<UpdateCargoTrackingCommand>
{
    public UpdateCargoTrackingCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CargoTrackingNo).NotEmpty().Length(15);
    }
}