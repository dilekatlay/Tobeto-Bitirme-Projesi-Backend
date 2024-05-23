using FluentValidation;

namespace Application.Features.Favorites.Commands.Update;

public class UpdateFavoriteCommandValidator : AbstractValidator<UpdateFavoriteCommand>
{
    public UpdateFavoriteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
    }
}