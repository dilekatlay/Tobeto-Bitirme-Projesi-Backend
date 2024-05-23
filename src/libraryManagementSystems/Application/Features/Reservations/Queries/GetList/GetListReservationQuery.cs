using Application.Features.Reservations.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;

namespace Application.Features.Reservations.Queries.GetList;

public class GetListReservationQuery : IRequest<GetListResponse<GetListReservationListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListReservations({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetReservations";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListReservationQueryHandler : IRequestHandler<GetListReservationQuery, GetListResponse<GetListReservationListItemDto>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetListReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListReservationListItemDto>> Handle(GetListReservationQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Reservation> reservations = await _reservationRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListReservationListItemDto> response = _mapper.Map<GetListResponse<GetListReservationListItemDto>>(reservations);
            return response;
        }
    }
}