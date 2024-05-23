using FluentValidation;

namespace Application.Features.CargoInformations.Commands.Update;

public class UpdateCargoInformationCommandValidator : AbstractValidator<UpdateCargoInformationCommand>
{
    public UpdateCargoInformationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CompanyName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.Adress).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.PhoneNumber).NotEmpty().Length(11);
    }
}