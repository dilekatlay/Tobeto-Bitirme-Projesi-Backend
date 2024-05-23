using Domain.Entities;
using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Books.Commands.Update;

public class UpdatedBookResponse : IResponse
{
    public Guid Id { get; set; }
    public string ISBNNo { get; set; }
    public string BookName { get; set; }
    public string Summary { get; set; }
    public string Writer { get; set; }
    public string imageUrl { get; set; }
    public int NumberOfCopies { get; set; }
    public int NumberOfPages { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid CategoryId { get; set; }

    public Guid ShelfId { get; set; }
    
}