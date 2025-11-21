namespace Domain.Entities;

public class Shift
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int WorkedHours { get; set; } = 0;
}