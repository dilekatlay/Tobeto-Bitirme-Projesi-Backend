using Application.Features.CreditCarts.Commands.Create;
using Application.Features.CreditCarts.Commands.Delete;
using Application.Features.CreditCarts.Commands.Update;
using Application.Features.CreditCarts.Queries.GetById;
using Application.Features.CreditCarts.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCartsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCreditCartCommand createCreditCartCommand)
    {
        CreatedCreditCartResponse response = await Mediator.Send(createCreditCartCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCreditCartCommand updateCreditCartCommand)
    {
        UpdatedCreditCartResponse response = await Mediator.Send(updateCreditCartCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCreditCartResponse response = await Mediator.Send(new DeleteCreditCartCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCreditCartResponse response = await Mediator.Send(new GetByIdCreditCartQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCreditCartQuery getListCreditCartQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCreditCartListItemDto> response = await Mediator.Send(getListCreditCartQuery);
        return Ok(response);
    }
}