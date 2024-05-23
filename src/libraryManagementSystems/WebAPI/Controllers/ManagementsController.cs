using Application.Features.Managements.Commands.Create;
using Application.Features.Managements.Commands.Delete;
using Application.Features.Managements.Commands.Update;
using Application.Features.Managements.Queries.GetById;
using Application.Features.Managements.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManagementsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateManagementCommand createManagementCommand)
    {
        CreatedManagementResponse response = await Mediator.Send(createManagementCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateManagementCommand updateManagementCommand)
    {
        UpdatedManagementResponse response = await Mediator.Send(updateManagementCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedManagementResponse response = await Mediator.Send(new DeleteManagementCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdManagementResponse response = await Mediator.Send(new GetByIdManagementQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListManagementQuery getListManagementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListManagementListItemDto> response = await Mediator.Send(getListManagementQuery);
        return Ok(response);
    }
}