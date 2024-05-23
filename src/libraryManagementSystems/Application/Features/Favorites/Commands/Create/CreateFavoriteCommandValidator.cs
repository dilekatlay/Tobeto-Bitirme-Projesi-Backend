using FluentValidation;

namespace Application.Features.Favorites.Commands.Create;

public class CreateFavoriteCommandValidator : AbstractValidator<CreateFavoriteCommand>
{
    public CreateFavoriteCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}