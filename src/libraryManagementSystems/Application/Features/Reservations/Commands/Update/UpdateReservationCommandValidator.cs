using FluentValidation;

namespace Application.Features.Reservations.Commands.Update;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.BookId).NotEmpty();
        RuleFor(c => c.MemberID).NotEmpty();
        RuleFor(c => c.ReservationDate).NotEmpty();
        RuleFor(c => c.ReservationEndDate).NotEmpty();

    }
}