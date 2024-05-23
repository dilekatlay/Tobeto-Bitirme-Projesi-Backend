using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BookRepository : EfRepositoryBase<Book, Guid, BaseDbContext>, IBookRepository
{
    public BookRepository(BaseDbContext context) : base(context)
    {
        
    }

    public async Task<List<Book>> GetBooksByCategoryAsync(Guid categoryId, int pageIndex, int pageSize)
        {
            var books = await Context.Books
                    .Where(b => b.CategoryId == categoryId)
                    .OrderBy(b => b.BookName)
                    .OrderBy(b => b.CategoryId)
                    .OrderBy(b => b.NumberOfCopies)
                    .OrderBy(b => b.NumberOfPages)
                    .OrderBy(b => b.Writer)// Ýsteðe baðlý: Kitap adýna göre sýralama
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return books;
        }
    
}