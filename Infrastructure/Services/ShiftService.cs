using System.Net;
using Domain.DTOs.Shift;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ShiftService(DataContext context) : IShiftService
{
    public async Task<Response<Shift>> StartShift(ShiftDto requestDto)
    {
        try
        {
            var employee = await context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefaultAsync(x => x.Id == requestDto.EmployeeId);

            if (employee is null)
                return new Response<Shift>(HttpStatusCode.NotFound, "Employee not found");

            if (employee.Shifts.Any(s => s.EndTime == null))
                return new Response<Shift>(HttpStatusCode.BadRequest, "You need to close the previous shift");

            var newShift = new Shift()
            {
                EmployeeId = requestDto.EmployeeId,
                StartTime = requestDto.Time,
                EndTime = null,
                WorkedHours = 0
            };

            await context.Shifts.AddAsync(newShift);
            await context.SaveChangesAsync();

            return new Response<Shift>(newShift);
        }
        catch (Exception e)
        {
            return new Response<Shift>(HttpStatusCode.InternalServerError, "Unexpected error");
        }
    }

    public async Task<Response<Shift>> EndShift(ShiftDto requestDto)
    {
        try
        {
            var employee = await context.Employees
                .Include(e => e.Shifts)
                .FirstOrDefaultAsync(x => x.Id == requestDto.EmployeeId);

            if (employee is null)
                return new Response<Shift>(HttpStatusCode.NotFound, "Employee not found");

            var activeShift = employee.Shifts.FirstOrDefault(s => s.EndTime == null);

            if (activeShift is null)
                return new Response<Shift>(HttpStatusCode.BadRequest, "There is no active shift to close");

            activeShift.EndTime = requestDto.Time;

            activeShift.WorkedHours =
                (int)(activeShift.EndTime.Value - activeShift.StartTime).TotalHours;

            await context.SaveChangesAsync();

            return new Response<Shift>(activeShift);
        }
        catch (Exception)
        {
            return new Response<Shift>(HttpStatusCode.InternalServerError, "Unexpected error");
        }
    }
}