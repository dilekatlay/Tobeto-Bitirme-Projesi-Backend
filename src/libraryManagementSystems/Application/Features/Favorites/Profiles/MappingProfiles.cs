using Application.Features.Favorites.Commands.Create;
using Application.Features.Favorites.Commands.Delete;
using Application.Features.Favorites.Commands.Update;
using Application.Features.Favorites.Queries.GetById;
using Application.Features.Favorites.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Favorites.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Favorite, CreateFavoriteCommand>().ReverseMap();
        CreateMap<Favorite, CreatedFavoriteResponse>().ReverseMap();
        CreateMap<Favorite, UpdateFavoriteCommand>().ReverseMap();
        CreateMap<Favorite, UpdatedFavoriteResponse>().ReverseMap();
        CreateMap<Favorite, DeleteFavoriteCommand>().ReverseMap();
        CreateMap<Favorite, DeletedFavoriteResponse>().ReverseMap();
        CreateMap<Favorite, GetByIdFavoriteResponse>().ReverseMap();
        CreateMap<Favorite, GetListFavoriteListItemDto>().ReverseMap();
        CreateMap<IPaginate<Favorite>, GetListResponse<GetListFavoriteListItemDto>>().ReverseMap();
    }
}