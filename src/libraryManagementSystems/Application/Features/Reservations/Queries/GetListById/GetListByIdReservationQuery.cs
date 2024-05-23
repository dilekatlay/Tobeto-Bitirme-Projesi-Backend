using Application.Features.Reservations.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;

namespace Application.Features.Reservations.Queries.GetListByMemberId
{
    public class GetListByMemberIdReservationQuery : IRequest<GetListResponse<GetListReservationListItemDto>>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public Guid Id { get; set; }

        public string[] Roles => [Admin, Read];

        public bool BypassCache { get; }
        public string? CacheKey => $"GetListReservations({PageRequest.PageIndex},{PageRequest.PageSize})";
        public string? CacheGroupKey => "GetReservations";
        public TimeSpan? SlidingExpiration { get; }

        public class GetListReservationQueryHandler : IRequestHandler<GetListByMemberIdReservationQuery, GetListResponse<GetListReservationListItemDto>>
        {
            private readonly IReservationRepository _reservationRepository;
            private readonly IMapper _mapper;

            public GetListReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
            {
                _reservationRepository = reservationRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListReservationListItemDto>> Handle(GetListByMemberIdReservationQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Reservation> reservations = await _reservationRepository.GetListAsync(
                    predicate: (reservation) => reservation.MemberID == request.Id && reservation.IsReserv==true,
                    include: (e) => e.Include(i => i.Book),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListReservationListItemDto> response = _mapper.Map<GetListResponse<GetListReservationListItemDto>>(reservations);
                return response;
            }
        }
    }
}