using Application.Features.Shelves.Commands.Create;
using Application.Features.Shelves.Commands.Delete;
using Application.Features.Shelves.Commands.Update;
using Application.Features.Shelves.Queries.GetById;
using Application.Features.Shelves.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Shelves.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Shelf, CreateShelfCommand>().ReverseMap();
        CreateMap<Shelf, CreatedShelfResponse>().ReverseMap();
        CreateMap<Shelf, UpdateShelfCommand>().ReverseMap();
        CreateMap<Shelf, UpdatedShelfResponse>().ReverseMap();
        CreateMap<Shelf, DeleteShelfCommand>().ReverseMap();
        CreateMap<Shelf, DeletedShelfResponse>().ReverseMap();
        CreateMap<Shelf, GetByIdShelfResponse>().ReverseMap();
        CreateMap<Shelf, GetListShelfListItemDto>().ReverseMap();
        CreateMap<IPaginate<Shelf>, GetListResponse<GetListShelfListItemDto>>().ReverseMap();
    }
}