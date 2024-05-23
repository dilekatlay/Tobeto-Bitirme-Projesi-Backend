using Application.Features.Categories.Constants;
using Application.Features.Categories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Categories.Constants.CategoriesOperationClaims;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;

namespace Application.Features.Categories.Commands.Create;

public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string CategoryName { get; set; }
    public IFormFile FormFile { get; set; }
    public string[] Roles => [Admin, Write, CategoriesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCategories"];

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryBusinessRules _categoryBusinessRules;
        private readonly ImageServiceBase _imageService;

        public CreateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
                                         CategoryBusinessRules categoryBusinessRules, ImageServiceBase imageService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _categoryBusinessRules = categoryBusinessRules;
            _imageService = imageService;
        }

        public async Task<CreatedCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = _mapper.Map<Category>(request);

            if (request.FormFile != null)

            {

                string imageUrl = await _imageService.UploadAsync(request.FormFile);

                category.imageUrl = imageUrl;

            }

            await _categoryRepository.AddAsync(category);

            CreatedCategoryResponse response = _mapper.Map<CreatedCategoryResponse>(category);
            response.imageUrl = category.imageUrl;
            return response;
        }
    }
}