using FluentValidation;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryName).NotEmpty();
        RuleFor(c => c.FormFile).NotEmpty();
    }
}