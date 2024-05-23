using FluentValidation;

namespace Application.Features.Members.Commands.Create;

public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.NationalIdentity).NotEmpty();
        RuleFor(c => c.Adress).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}