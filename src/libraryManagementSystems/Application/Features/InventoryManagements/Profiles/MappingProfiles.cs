using Application.Features.InventoryManagements.Commands.Create;
using Application.Features.InventoryManagements.Commands.Delete;
using Application.Features.InventoryManagements.Commands.Update;
using Application.Features.InventoryManagements.Queries.GetById;
using Application.Features.InventoryManagements.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.InventoryManagements.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<InventoryManagement, CreateInventoryManagementCommand>().ReverseMap();
        CreateMap<InventoryManagement, CreatedInventoryManagementResponse>().ReverseMap();
        CreateMap<InventoryManagement, UpdateInventoryManagementCommand>().ReverseMap();
        CreateMap<InventoryManagement, UpdatedInventoryManagementResponse>().ReverseMap();
        CreateMap<InventoryManagement, DeleteInventoryManagementCommand>().ReverseMap();
        CreateMap<InventoryManagement, DeletedInventoryManagementResponse>().ReverseMap();
        CreateMap<InventoryManagement, GetByIdInventoryManagementResponse>().ReverseMap();
        CreateMap<InventoryManagement, GetListInventoryManagementListItemDto>().ReverseMap();
        CreateMap<IPaginate<InventoryManagement>, GetListResponse<GetListInventoryManagementListItemDto>>().ReverseMap();
    }
}