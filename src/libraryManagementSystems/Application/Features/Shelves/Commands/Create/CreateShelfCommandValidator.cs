using FluentValidation;

namespace Application.Features.Shelves.Commands.Create;

public class CreateShelfCommandValidator : AbstractValidator<CreateShelfCommand>
{
    public CreateShelfCommandValidator()
    {
        RuleFor(c => c.ShelfNo).NotEmpty().Must(shelfNumber => shelfNumber >= 1 && shelfNumber <= 100);
        RuleFor(c => c.ShelfLocation).NotEmpty();
        RuleFor(c => c.Capacity).NotEmpty().InclusiveBetween(0, 200);
        RuleFor(c => c.NumberOfBooksAvailable).NotEmpty();
    }
}