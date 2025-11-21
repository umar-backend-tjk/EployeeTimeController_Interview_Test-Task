using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Shift;

public class ShiftDto
{
    [Required]
    public required Guid EmployeeId { get; set; }
    [Required]
    public required DateTime Time { get; set; }
}