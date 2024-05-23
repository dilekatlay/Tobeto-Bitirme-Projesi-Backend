using Application.Features.CargoInformations.Commands.Create;
using Application.Features.CargoInformations.Commands.Delete;
using Application.Features.CargoInformations.Commands.Update;
using Application.Features.CargoInformations.Queries.GetById;
using Application.Features.CargoInformations.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CargoInformations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CargoInformation, CreateCargoInformationCommand>().ReverseMap();
        CreateMap<CargoInformation, CreatedCargoInformationResponse>().ReverseMap();
        CreateMap<CargoInformation, UpdateCargoInformationCommand>().ReverseMap();
        CreateMap<CargoInformation, UpdatedCargoInformationResponse>().ReverseMap();
        CreateMap<CargoInformation, DeleteCargoInformationCommand>().ReverseMap();
        CreateMap<CargoInformation, DeletedCargoInformationResponse>().ReverseMap();
        CreateMap<CargoInformation, GetByIdCargoInformationResponse>().ReverseMap();
        CreateMap<CargoInformation, GetListCargoInformationListItemDto>().ReverseMap();
        CreateMap<IPaginate<CargoInformation>, GetListResponse<GetListCargoInformationListItemDto>>().ReverseMap();
    }
}