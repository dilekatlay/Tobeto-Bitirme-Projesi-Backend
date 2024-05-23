using Application.Features.Reservations.Constants;
using Application.Features.Reservations.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Reservations.Constants.ReservationsOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetById;

public class GetByIdReservationQuery : IRequest<GetByIdReservationResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdReservationQueryHandler : IRequestHandler<GetByIdReservationQuery, GetByIdReservationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ReservationBusinessRules _reservationBusinessRules;
        private readonly IBookRepository _bookRepository;

        public GetByIdReservationQueryHandler(IMapper mapper, IReservationRepository reservationRepository, IBookRepository bookRepository,

            ReservationBusinessRules reservationBusinessRules)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _reservationBusinessRules = reservationBusinessRules;
            _bookRepository = bookRepository;
        }

        public async Task<GetByIdReservationResponse> Handle(GetByIdReservationQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = await _reservationRepository.GetAsync(predicate: r => r.MemberID == request.Id, cancellationToken: cancellationToken);
            await _reservationBusinessRules.ReservationShouldExistWhenSelected(reservation);

            Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == reservation.BookId, cancellationToken: cancellationToken);

            GetByIdReservationResponse response = _mapper.Map<GetByIdReservationResponse>(reservation);

            response.BookName = book.BookName;
            return response;
        }
    }
}