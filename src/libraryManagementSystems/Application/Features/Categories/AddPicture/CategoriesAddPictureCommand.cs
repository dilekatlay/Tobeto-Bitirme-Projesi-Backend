using Application.Services.ImageService;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Commands.AddPicture;
public class CategoriesAddPictureCommand : IRequest<CategoriesAddPictureResponse>
{
    public IFormFile File { get; set; }

    public class CategoriesAddPictureCommandHandler : IRequestHandler<CategoriesAddPictureCommand, CategoriesAddPictureResponse>
    {
        private readonly ImageServiceBase _imageService;

        public CategoriesAddPictureCommandHandler(ImageServiceBase imageService)
        {
            _imageService = imageService;
        }

        public async Task<CategoriesAddPictureResponse> Handle(CategoriesAddPictureCommand request, CancellationToken cancellationToken)
        {
            string url = await _imageService.UploadAsync(request.File);
            return new CategoriesAddPictureResponse() { Url = url };
        }
    }
}