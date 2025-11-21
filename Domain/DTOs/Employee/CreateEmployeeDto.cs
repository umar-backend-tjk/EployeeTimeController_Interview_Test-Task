using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs;

public class CreateEmployeeDto
{
    [Required]
    public required string FirstName { get; set; }
    
    [Required]
    public required string LastName { get; set; }
    
    public string? Patronymic { get; set; }
    
    [Required]
    public JobPosition Position { get; set; }
}