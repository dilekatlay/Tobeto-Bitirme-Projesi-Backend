using Application.Features.CargoInformations.Commands.Create;
using Application.Features.CargoInformations.Commands.Delete;
using Application.Features.CargoInformations.Commands.Update;
using Application.Features.CargoInformations.Queries.GetById;
using Application.Features.CargoInformations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CargoInformationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCargoInformationCommand createCargoInformationCommand)
    {
        CreatedCargoInformationResponse response = await Mediator.Send(createCargoInformationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCargoInformationCommand updateCargoInformationCommand)
    {
        UpdatedCargoInformationResponse response = await Mediator.Send(updateCargoInformationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCargoInformationResponse response = await Mediator.Send(new DeleteCargoInformationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCargoInformationResponse response = await Mediator.Send(new GetByIdCargoInformationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCargoInformationQuery getListCargoInformationQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCargoInformationListItemDto> response = await Mediator.Send(getListCargoInformationQuery);
        return Ok(response);
    }
}