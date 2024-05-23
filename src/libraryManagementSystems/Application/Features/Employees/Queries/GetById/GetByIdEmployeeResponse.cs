using NArchitecture.Core.Application.Responses;

namespace Application.Features.Employees.Queries.GetById;

public class GetByIdEmployeeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string Contact { get; set; }
    public string WorkingHours { get; set; }
}