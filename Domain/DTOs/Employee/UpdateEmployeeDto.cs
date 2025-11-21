using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs;

public class UpdateEmployeeDto
{
    [Required]
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public JobPosition? Position { get; set; }
}