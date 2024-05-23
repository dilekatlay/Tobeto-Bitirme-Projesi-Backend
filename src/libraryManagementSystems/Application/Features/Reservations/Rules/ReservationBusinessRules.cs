using Application.Features.Reservations.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using AutoMapper.Execution;

namespace Application.Features.Reservations.Rules;

public class ReservationBusinessRules : BaseBusinessRules
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ILocalizationService _localizationService;

    public ReservationBusinessRules(IReservationRepository reservationRepository, ILocalizationService localizationService)
    {
        _reservationRepository = reservationRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ReservationsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ReservationShouldExistWhenSelected(Reservation? reservation)
    {
        if (reservation == null)
            await throwBusinessException(ReservationsBusinessMessages.ReservationNotExists);
    }

    public async Task ReservationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Reservation? reservation = await _reservationRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ReservationShouldExistWhenSelected(reservation);
    }

    //Yeni üye rezervasyon yapamaz................................................................
    //public static bool NewMemberCannotMakeaReservation(Reservation reservation, Member member)
    //{
    //    if(member.membershiptype == new)
    //    {
    //        return false;
    //    }
    //    return true;
        
    //}


}