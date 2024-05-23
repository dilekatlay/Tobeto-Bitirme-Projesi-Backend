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

namespace Application.Features.Reservations.Commands.Update;

public class UpdateReservationCommand : IRequest<UpdatedReservationResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberID { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationEndDate { get; set; }
    public Boolean IsReserv { get; set; }

    public string[] Roles => [Admin, Write, ReservationsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetReservations"];

    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, UpdatedReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;

        public UpdateReservationCommandHandler(IMapper mapper, IReservationRepository reservationRepository,
                                         ReservationBusinessRules reservationBusinessRules)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationBusinessRules = reservationBusinessRules;
        }

        public async Task<UpdatedReservationResponse> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation? reservation = await _reservationRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);
            
            reservation = _mapper.Map(request, reservation);
            reservation.UpdatedDate= DateTime.Now;
            reservation.IsReserv=false;
            await _reservationRepository.UpdateAsync(reservation!);

            UpdatedReservationResponse response = _mapper.Map<UpdatedReservationResponse>(reservation);
            return response;
        }
    }
}