using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.Employees.Commands.Update;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.UserName).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.Password).NotEmpty().Length(6, 9).WithMessage("�ifre en az 6 en fazla 9 karakter i�ermelidir.")
    .Must(password =>
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,9}$");
    }).WithMessage("�ifre bir k���k harf, bir b�y�k harf ve bir �zel karakter i�ermeli ve 6-9 karakter uzunlu�unda olmal�d�r.");
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.Contact).NotEmpty().Length(11);
        RuleFor(c => c.WorkingHours).NotEmpty().Equal("09.00-18.00").WithMessage("Varsay�lan �al��ma saatleri 09.00 ile 18.00 aras�ndad�r.");
    }
}