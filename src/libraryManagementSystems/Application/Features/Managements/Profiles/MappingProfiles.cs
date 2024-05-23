using Application.Features.Managements.Commands.Create;
using Application.Features.Managements.Commands.Delete;
using Application.Features.Managements.Commands.Update;
using Application.Features.Managements.Queries.GetById;
using Application.Features.Managements.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Managements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Management, CreateManagementCommand>().ReverseMap();
        CreateMap<Management, CreatedManagementResponse>().ReverseMap();
        CreateMap<Management, UpdateManagementCommand>().ReverseMap();
        CreateMap<Management, UpdatedManagementResponse>().ReverseMap();
        CreateMap<Management, DeleteManagementCommand>().ReverseMap();
        CreateMap<Management, DeletedManagementResponse>().ReverseMap();
        CreateMap<Management, GetByIdManagementResponse>().ReverseMap();
        CreateMap<Management, GetListManagementListItemDto>().ReverseMap();
        CreateMap<IPaginate<Management>, GetListResponse<GetListManagementListItemDto>>().ReverseMap();
    }
}