using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CreditCarts;

public interface ICreditCartService
{
    Task<CreditCart?> GetAsync(
        Expression<Func<CreditCart, bool>> predicate,
        Func<IQueryable<CreditCart>, IIncludableQueryable<CreditCart, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CreditCart>?> GetListAsync(
        Expression<Func<CreditCart, bool>>? predicate = null,
        Func<IQueryable<CreditCart>, IOrderedQueryable<CreditCart>>? orderBy = null,
        Func<IQueryable<CreditCart>, IIncludableQueryable<CreditCart, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CreditCart> AddAsync(CreditCart creditCart);
    Task<CreditCart> UpdateAsync(CreditCart creditCart);
    Task<CreditCart> DeleteAsync(CreditCart creditCart, bool permanent = false);
}
