using NArchitecture.Core.Application.Responses;

namespace Application.Features.Reservations.Commands.Create;

public class CreatedReservationResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
    public Boolean IsReserv { get; set; }
}