using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Shelves.Commands.Create;

public class CreatedShelfResponse : IResponse
{
    public Guid Id { get; set; }
    public int ShelfNo { get; set; }
    public string ShelfLocation { get; set; }
    public int Capacity { get; set; }
    public bool NumberOfBooksAvailable { get; set; }
}