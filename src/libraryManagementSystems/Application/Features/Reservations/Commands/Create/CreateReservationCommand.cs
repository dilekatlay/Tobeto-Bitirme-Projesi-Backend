using Application.Features.Reservations.Constants;
using Application.Features.Reservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;

namespace Application.Features.Reservations.Commands.Create;

public class CreateReservationCommand : IRequest<CreatedReservationResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid BookId { get; set; }
    public Guid MemberID { get; set; }
    public Boolean IsReserv { get; set; }

    public string[] Roles => [Admin, Write, ReservationsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReservations"];

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, CreatedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public CreateReservationCommandHandler(IMapper mapper, IReservationRepository reservationRepository,
                                         ReservationBusinessRules reservationBusinessRules)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationBusinessRules = reservationBusinessRules;
        }

        public async Task<CreatedReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = _mapper.Map<Reservation>(request);
            reservation.IsReserv = true;

            reservation.ReservationDate = DateTime.Now;
            reservation.ReservationEndDate = reservation.ReservationDate.AddDays(15);

            await _reservationRepository.AddAsync(reservation);

            CreatedReservationResponse response = _mapper.Map<CreatedReservationResponse>(reservation);
            return response;
        }
    }
}