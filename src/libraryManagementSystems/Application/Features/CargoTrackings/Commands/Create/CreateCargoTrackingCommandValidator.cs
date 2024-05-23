using FluentValidation;

namespace Application.Features.CargoTrackings.Commands.Create;

public class CreateCargoTrackingCommandValidator : AbstractValidator<CreateCargoTrackingCommand>
{
    public CreateCargoTrackingCommandValidator()
    {
        RuleFor(c => c.CargoTrackingNo).NotEmpty().Length(15);
    }
}