using FluentValidation;

namespace Application.Features.CargoInformations.Commands.Delete;

public class DeleteCargoInformationCommandValidator : AbstractValidator<DeleteCargoInformationCommand>
{
    public DeleteCargoInformationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}