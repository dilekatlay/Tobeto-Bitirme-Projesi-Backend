using Application.Features.InventoryManagements.Commands.Create;
using Application.Features.InventoryManagements.Commands.Delete;
using Application.Features.InventoryManagements.Commands.Update;
using Application.Features.InventoryManagements.Queries.GetById;
using Application.Features.InventoryManagements.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryManagementsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateInventoryManagementCommand createInventoryManagementCommand)
    {
        CreatedInventoryManagementResponse response = await Mediator.Send(createInventoryManagementCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInventoryManagementCommand updateInventoryManagementCommand)
    {
        UpdatedInventoryManagementResponse response = await Mediator.Send(updateInventoryManagementCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedInventoryManagementResponse response = await Mediator.Send(new DeleteInventoryManagementCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdInventoryManagementResponse response = await Mediator.Send(new GetByIdInventoryManagementQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListInventoryManagementQuery getListInventoryManagementQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListInventoryManagementListItemDto> response = await Mediator.Send(getListInventoryManagementQuery);
        return Ok(response);
    }
}