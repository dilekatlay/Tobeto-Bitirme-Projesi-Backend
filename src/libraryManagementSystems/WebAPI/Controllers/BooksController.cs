using Application.Features.Books.Commands.Create;
using Application.Features.Books.Commands.Delete;
using Application.Features.Books.Commands.Update;
using Application.Features.Books.Queries.GetById;
using Application.Features.Books.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Customers.Commands.AddPicture;
using Application.Services.Books;
using Microsoft.EntityFrameworkCore;
using Application.Services.Repositories;
using Nest;
using Persistence.Contexts;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : BaseController
{
    private readonly IBookRepository _bookRepository;
    private readonly BaseDbContext _context;

    public BooksController(IBookRepository bookRepository, BaseDbContext context)
    {
        _bookRepository = bookRepository;
        _context = context;
    }

    [HttpPut("{id}/reduceStock")]
    public IActionResult ReduceStock(Guid id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        if (book.NumberOfCopies > 0)
        {
            book.NumberOfCopies--;
            _context.SaveChanges();

            return Ok(book);
        }
        else
        {
            return BadRequest("Stok adedi zaten sýfýr.");
        }
    }

    [HttpPut("increaseStock/{id}")]
    public async Task<IActionResult> IncreaseStock(Guid id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        // Stok adedini arttýr
        book.NumberOfCopies++;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("ByCategory")]
    public async Task<IActionResult> GetBooksByCategory([FromQuery] Guid categoryId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        try
        {
            // Kategoriye göre kitaplarý getir
            var books = await _bookRepository.GetBooksByCategoryAsync(categoryId, pageIndex, pageSize);

            // Kitaplar bulunduysa
            if (books != null && books.Count > 0)
            {
                // Baþarýlý yanýt döndür
                return Ok(books);
            }
            else
            {
                // Belirtilen kategoriye ait kitap bulunamadýysa NotFound yanýtý döndür
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            // Hata oluþtuðunda Internal Server Error döndür
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }



    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateBookCommand createBookCommand)
    {
        CreatedBookResponse response = await Mediator.Send(createBookCommand);

        return Created(uri: "", response);
    }

    [HttpPost("Image")]
    public async Task<IActionResult> AddImage([FromForm] IFormFile formFile)
    {
        AddPictureCommand command = new AddPictureCommand() { File = formFile };
        var response = await Mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBookCommand updateBookCommand)
    {
        UpdatedBookResponse response = await Mediator.Send(updateBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBookResponse response = await Mediator.Send(new DeleteBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBookResponse response = await Mediator.Send(new GetByIdBookQuery { Id = id });
        return Ok(response);
    }

   


    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBookQuery getListBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBookListItemDto> response = await Mediator.Send(getListBookQuery);
        return Ok(response);
    }
}