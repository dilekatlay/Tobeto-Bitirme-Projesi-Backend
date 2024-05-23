using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Reservations.Queries.GetList;

public class GetListReservationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
    public Boolean IsReserv { get; set; }
    public Book Book { get; set; }
}