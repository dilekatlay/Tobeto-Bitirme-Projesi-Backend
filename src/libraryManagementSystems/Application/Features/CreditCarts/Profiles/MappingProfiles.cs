using Application.Features.CreditCarts.Commands.Create;
using Application.Features.CreditCarts.Commands.Delete;
using Application.Features.CreditCarts.Commands.Update;
using Application.Features.CreditCarts.Queries.GetById;
using Application.Features.CreditCarts.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.CreditCarts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreditCart, CreateCreditCartCommand>().ReverseMap();
        CreateMap<CreditCart, CreatedCreditCartResponse>().ReverseMap();
        CreateMap<CreditCart, UpdateCreditCartCommand>().ReverseMap();
        CreateMap<CreditCart, UpdatedCreditCartResponse>().ReverseMap();
        CreateMap<CreditCart, DeleteCreditCartCommand>().ReverseMap();
        CreateMap<CreditCart, DeletedCreditCartResponse>().ReverseMap();
        CreateMap<CreditCart, GetByIdCreditCartResponse>().ReverseMap();
        CreateMap<CreditCart, GetListCreditCartListItemDto>().ReverseMap();
        CreateMap<IPaginate<CreditCart>, GetListResponse<GetListCreditCartListItemDto>>().ReverseMap();
    }
}