using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryRepository : EfRepositoryBase<Category, Guid, BaseDbContext>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<Category>> GetCategoriesByCategoryAsync( int pageIndex, int pageSize)
    {
        var categories = await Context.Categories
                .OrderBy(b => b.CategoryName)
                // Ýsteðe baðlý: Kitap adýna göre sýralama
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

        return categories;
    }
}