using Application.Features.Books.Dtos;
using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reservations.Queries.GetListById;
public class GetListByIdReservationDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
    public Boolean IsReserv { get; set; }

}