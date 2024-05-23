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
        RuleFor(c => c.Password).NotEmpty().Length(6, 9).WithMessage("Þifre en az 6 en fazla 9 karakter içermelidir.")
    .Must(password =>
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,9}$");
    }).WithMessage("Þifre bir küçük harf, bir büyük harf ve bir özel karakter içermeli ve 6-9 karakter uzunluðunda olmalýdýr.");
        RuleFor(c => c.FirstName).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.LastName).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(20);
        RuleFor(c => c.Contact).NotEmpty().Length(11);
        RuleFor(c => c.WorkingHours).NotEmpty().Equal("09.00-18.00").WithMessage("Varsayýlan çalýþma saatleri 09.00 ile 18.00 arasýndadýr.");
    }
}