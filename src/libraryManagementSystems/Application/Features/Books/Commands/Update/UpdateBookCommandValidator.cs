using Domain.Enums;
using FluentValidation;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ISBNNo).NotEmpty().MinimumLength(10).MaximumLength(13);
        RuleFor(c => c.BookName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(c => c.Summary).NotEmpty().MinimumLength(2);
        RuleFor(c => c.Writer).NotEmpty().MinimumLength(2);
        RuleFor(c => c.imageUrl).NotEmpty().MinimumLength(2);
        RuleFor(c => c.NumberOfCopies).NotEmpty();
        RuleFor(c => c.NumberOfPages).NotEmpty().InclusiveBetween(0, 2000);
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(c => c.ShelfId).NotEmpty();
        RuleFor(c => c.UnitPrice).NotEmpty();

    }



}