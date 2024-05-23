using Application.Features.CargoTrackings.Commands.Create;
using Application.Features.CargoTrackings.Commands.Delete;
using Application.Features.CargoTrackings.Commands.Update;
using Application.Features.CargoTrackings.Queries.GetById;
using Application.Features.CargoTrackings.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CargoTrackings.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CargoTracking, CreateCargoTrackingCommand>().ReverseMap();
        CreateMap<CargoTracking, CreatedCargoTrackingResponse>().ReverseMap();
        CreateMap<CargoTracking, UpdateCargoTrackingCommand>().ReverseMap();
        CreateMap<CargoTracking, UpdatedCargoTrackingResponse>().ReverseMap();
        CreateMap<CargoTracking, DeleteCargoTrackingCommand>().ReverseMap();
        CreateMap<CargoTracking, DeletedCargoTrackingResponse>().ReverseMap();
        CreateMap<CargoTracking, GetByIdCargoTrackingResponse>().ReverseMap();
        CreateMap<CargoTracking, GetListCargoTrackingListItemDto>().ReverseMap();
        CreateMap<IPaginate<CargoTracking>, GetListResponse<GetListCargoTrackingListItemDto>>().ReverseMap();
    }
}