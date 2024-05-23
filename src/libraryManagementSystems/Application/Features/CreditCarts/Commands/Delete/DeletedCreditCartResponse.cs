using NArchitecture.Core.Application.Responses;

namespace Application.Features.CreditCarts.Commands.Delete;

public class DeletedCreditCartResponse : IResponse
{
    public Guid Id { get; set; }
}