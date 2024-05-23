using Application.Features.Shelves.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Shelves;

public class ShelfManager : IShelfService
{
    private readonly IShelfRepository _shelfRepository;
    private readonly ShelfBusinessRules _shelfBusinessRules;

    public ShelfManager(IShelfRepository shelfRepository, ShelfBusinessRules shelfBusinessRules)
    {
        _shelfRepository = shelfRepository;
        _shelfBusinessRules = shelfBusinessRules;
    }

    public async Task<Shelf?> GetAsync(
        Expression<Func<Shelf, bool>> predicate,
        Func<IQueryable<Shelf>, IIncludableQueryable<Shelf, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Shelf? shelf = await _shelfRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return shelf;
    }

    public async Task<IPaginate<Shelf>?> GetListAsync(
        Expression<Func<Shelf, bool>>? predicate = null,
        Func<IQueryable<Shelf>, IOrderedQueryable<Shelf>>? orderBy = null,
        Func<IQueryable<Shelf>, IIncludableQueryable<Shelf, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Shelf> shelfList = await _shelfRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return shelfList;
    }

    public async Task<Shelf> AddAsync(Shelf shelf)
    {
        Shelf addedShelf = await _shelfRepository.AddAsync(shelf);

        return addedShelf;
    }

    public async Task<Shelf> UpdateAsync(Shelf shelf)
    {
        Shelf updatedShelf = await _shelfRepository.UpdateAsync(shelf);

        return updatedShelf;
    }

    public async Task<Shelf> DeleteAsync(Shelf shelf, bool permanent = false)
    {
        Shelf deletedShelf = await _shelfRepository.DeleteAsync(shelf);

        return deletedShelf;
    }
}
