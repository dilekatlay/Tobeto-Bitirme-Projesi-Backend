using Application.Features.CargoTrackings.Commands.Create;
using Application.Features.CargoTrackings.Commands.Delete;
using Application.Features.CargoTrackings.Commands.Update;
using Application.Features.CargoTrackings.Queries.GetById;
using Application.Features.CargoTrackings.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CargoTrackingsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCargoTrackingCommand createCargoTrackingCommand)
    {
        CreatedCargoTrackingResponse response = await Mediator.Send(createCargoTrackingCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCargoTrackingCommand updateCargoTrackingCommand)
    {
        UpdatedCargoTrackingResponse response = await Mediator.Send(updateCargoTrackingCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCargoTrackingResponse response = await Mediator.Send(new DeleteCargoTrackingCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCargoTrackingResponse response = await Mediator.Send(new GetByIdCargoTrackingQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCargoTrackingQuery getListCargoTrackingQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCargoTrackingListItemDto> response = await Mediator.Send(getListCargoTrackingQuery);
        return Ok(response);
    }
}