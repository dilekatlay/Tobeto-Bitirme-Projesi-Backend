using FluentValidation;

namespace Application.Features.CargoInformations.Commands.Create;

public class CreateCargoInformationCommandValidator : AbstractValidator<CreateCargoInformationCommand>
{
    public CreateCargoInformationCommandValidator()
    {
        RuleFor(c => c.CompanyName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.Adress).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.PhoneNumber).NotEmpty().Length(11);
    }
}