using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        CreatedEmployeeResponse response = await Mediator.Send(createEmployeeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
    {
        UpdatedEmployeeResponse response = await Mediator.Send(updateEmployeeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEmployeeResponse response = await Mediator.Send(new DeleteEmployeeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEmployeeResponse response = await Mediator.Send(new GetByIdEmployeeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeQuery getListEmployeeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEmployeeListItemDto> response = await Mediator.Send(getListEmployeeQuery);
        return Ok(response);
    }
    [HttpPost("email")]
    public async Task<IActionResult> SendMail([FromBody] string firstname,string lastname ,string email, string subject)
    {
        firstname = firstname ?? string.Empty;
        lastname = lastname ?? string.Empty;
        Mail(subject, email);
        void Mail(string subject, string email)
        {

            string from = "emirkarameke4444@gmail.com";
            //string subject = "Test Mail";
            // string body = "Bu bir test mailidir.";
            string acik = "Kullanýcý Önerisi";
            MimeMessage message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(email));
            message.To.Add(MailboxAddress.Parse(from));
            message.Subject = acik;
            message.Body = new TextPart("plain") { Text = subject };

            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("sandbox.smtp.mailtrap.io", 587, false);
                client.Authenticate("d92fb68cb4c2c0", "fc7235e74b66b8");
                client.Send(message);
                client.Disconnect(true);
            }

            
        }
        return Ok(201);
    }
}