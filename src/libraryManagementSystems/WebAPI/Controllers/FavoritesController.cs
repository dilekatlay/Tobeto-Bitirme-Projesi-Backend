using Application.Features.Favorites.Commands.Create;
using Application.Features.Favorites.Commands.Delete;
using Application.Features.Favorites.Commands.Update;
using Application.Features.Favorites.Queries.GetById;
using Application.Features.Favorites.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFavoriteCommand createFavoriteCommand)
    {
        CreatedFavoriteResponse response = await Mediator.Send(createFavoriteCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFavoriteCommand updateFavoriteCommand)
    {
        UpdatedFavoriteResponse response = await Mediator.Send(updateFavoriteCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFavoriteResponse response = await Mediator.Send(new DeleteFavoriteCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFavoriteResponse response = await Mediator.Send(new GetByIdFavoriteQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFavoriteQuery getListFavoriteQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFavoriteListItemDto> response = await Mediator.Send(getListFavoriteQuery);
        return Ok(response);
    }
}