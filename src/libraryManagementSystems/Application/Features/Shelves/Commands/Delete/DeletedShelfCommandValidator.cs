using FluentValidation;

namespace Application.Features.Shelves.Commands.Delete;

public class DeleteShelfCommandValidator : AbstractValidator<DeleteShelfCommand>
{
    public DeleteShelfCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}