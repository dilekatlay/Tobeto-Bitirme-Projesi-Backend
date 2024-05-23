using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Shelves.Queries.GetList;

public class GetListShelfListItemDto : IDto
{
    public Guid Id { get; set; }
    public int ShelfNo { get; set; }
    public string ShelfLocation { get; set; }
    public int Capacity { get; set; }
    public bool NumberOfBooksAvailable { get; set; }
}