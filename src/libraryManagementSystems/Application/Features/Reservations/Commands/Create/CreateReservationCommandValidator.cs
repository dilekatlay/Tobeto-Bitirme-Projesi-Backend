using FluentValidation;

namespace Application.Features.Reservations.Commands.Create;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.MemberID).NotEmpty();
        RuleFor(c => c.IsReserv).NotEmpty();
    }
}