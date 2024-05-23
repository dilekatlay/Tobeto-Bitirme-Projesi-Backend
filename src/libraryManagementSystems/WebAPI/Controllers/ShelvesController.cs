using Application.Features.Shelves.Commands.Create;
using Application.Features.Shelves.Commands.Delete;
using Application.Features.Shelves.Commands.Update;
using Application.Features.Shelves.Queries.GetById;
using Application.Features.Shelves.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShelvesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateShelfCommand createShelfCommand)
    {
        CreatedShelfResponse response = await Mediator.Send(createShelfCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateShelfCommand updateShelfCommand)
    {
        UpdatedShelfResponse response = await Mediator.Send(updateShelfCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedShelfResponse response = await Mediator.Send(new DeleteShelfCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdShelfResponse response = await Mediator.Send(new GetByIdShelfQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListShelfQuery getListShelfQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListShelfListItemDto> response = await Mediator.Send(getListShelfQuery);
        return Ok(response);
    }
}