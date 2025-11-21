using Domain.Enums;

namespace Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Patronymic { get; set; }
    public JobPosition Position { get; set; }
}