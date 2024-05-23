using Application.Features.Books.Constants;
using Application.Features.Books.Rules;
using Application.Services.ImageService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using static Application.Features.Books.Constants.BooksOperationClaims;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommand : IRequest<CreatedBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest

{
    public string ISBNNo { get; set; }
    public string BookName { get; set; }
    public string Summary { get; set; }
    public string Writer { get; set; }
    public int NumberOfCopies { get; set; }
    public int NumberOfPages { get; set; }
    public decimal UnitPrice { get; set; }
    public IFormFile FormFile { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ShelfId { get; set; }
    public string[] Roles => [Admin, Write, BooksOperationClaims.Create];
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBooks"];
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreatedBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;
        private readonly ImageServiceBase _imageService;
        public CreateBookCommandHandler(IMapper mapper, IBookRepository bookRepository, ImageServiceBase imageService,

                                        BookBusinessRules bookBusinessRules)

        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
            _imageService = imageService;
        }

        public async Task<CreatedBookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Book book = _mapper.Map<Book>(request);
            if (request.FormFile != null)
            {
                string imageUrl = await _imageService.UploadAsync(request.FormFile);
                book.imageUrl = imageUrl;
            }
            await _bookRepository.AddAsync(book);
            CreatedBookResponse response = _mapper.Map<CreatedBookResponse>(book);
            response.imageUrl = book.imageUrl;
            return response;
        }
    }
}