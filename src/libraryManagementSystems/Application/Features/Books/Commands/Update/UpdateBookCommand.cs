using Application.Features.Books.Constants;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Books.Constants.BooksOperationClaims;

namespace Application.Features.Books.Commands.Update;

public class UpdateBookCommand : IRequest<UpdatedBookResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
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
    public string[] Roles => [Admin, Write, BooksOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBooks"];

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdatedBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly BookBusinessRules _bookBusinessRules;

        public UpdateBookCommandHandler(IMapper mapper, IBookRepository bookRepository,
                                         BookBusinessRules bookBusinessRules)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _bookBusinessRules = bookBusinessRules;
        }

        public async Task<UpdatedBookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Book? book = await _bookRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _bookBusinessRules.BookShouldExistWhenSelected(book);
            book = _mapper.Map(request, book);

            await _bookRepository.UpdateAsync(book!);

            UpdatedBookResponse response = _mapper.Map<UpdatedBookResponse>(book);
            return response;
        }
    }
}