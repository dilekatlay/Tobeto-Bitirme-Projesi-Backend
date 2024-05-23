using Application.Features.CreditCarts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CreditCarts;

public class CreditCartManager : ICreditCartService
{
    private readonly ICreditCartRepository _creditCartRepository;
    private readonly CreditCartBusinessRules _creditCartBusinessRules;

    public CreditCartManager(ICreditCartRepository creditCartRepository, CreditCartBusinessRules creditCartBusinessRules)
    {
        _creditCartRepository = creditCartRepository;
        _creditCartBusinessRules = creditCartBusinessRules;
    }

    public async Task<CreditCart?> GetAsync(
        Expression<Func<CreditCart, bool>> predicate,
        Func<IQueryable<CreditCart>, IIncludableQueryable<CreditCart, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CreditCart? creditCart = await _creditCartRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return creditCart;
    }

    public async Task<IPaginate<CreditCart>?> GetListAsync(
        Expression<Func<CreditCart, bool>>? predicate = null,
        Func<IQueryable<CreditCart>, IOrderedQueryable<CreditCart>>? orderBy = null,
        Func<IQueryable<CreditCart>, IIncludableQueryable<CreditCart, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CreditCart> creditCartList = await _creditCartRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return creditCartList;
    }

    public async Task<CreditCart> AddAsync(CreditCart creditCart)
    {
        CreditCart addedCreditCart = await _creditCartRepository.AddAsync(creditCart);

        return addedCreditCart;
    }

    public async Task<CreditCart> UpdateAsync(CreditCart creditCart)
    {
        CreditCart updatedCreditCart = await _creditCartRepository.UpdateAsync(creditCart);

        return updatedCreditCart;
    }

    public async Task<CreditCart> DeleteAsync(CreditCart creditCart, bool permanent = false)
    {
        CreditCart deletedCreditCart = await _creditCartRepository.DeleteAsync(creditCart);

        return deletedCreditCart;
    }
}
