using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class EmployeeService(DataContext context) : IEmployeeService
{
    public async Task<Response<Employee>> CreateEmployeeAsync(CreateEmployeeDto model)
    {
        try
        {
            var employee = new Employee()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Position = model.Position
            };
            
            await context.Employees.AddAsync(employee);
            var result = await context.SaveChangesAsync();

            if (result == 0)
            {
                return new Response<Employee>(HttpStatusCode.BadRequest, "Failed to add an employee");
            }
            
            return new Response<Employee>(employee);
        }
        catch (Exception e)
        {
            return new Response<Employee>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<Employee>> UpdateEmployeeAsync(UpdateEmployeeDto model)
    {
        try
        {
            var existingEmployee = await context.Employees.FindAsync(model.Id);
            if (existingEmployee == null)
            {
                return new Response<Employee>(HttpStatusCode.NotFound, "Not found the employee to update");
            }
            
            existingEmployee.FirstName = model.FirstName ?? existingEmployee.FirstName;
            existingEmployee.LastName = model.LastName ?? existingEmployee.LastName;
            existingEmployee.Patronymic = model.Patronymic ?? existingEmployee.Patronymic;
            existingEmployee.Position = model.Position ?? existingEmployee.Position;
            
            var result = await context.SaveChangesAsync();

            if (result == 0)
            {
                return new Response<Employee>(HttpStatusCode.BadRequest, "Failed to update the employee");
            }
            
            return new Response<Employee>(existingEmployee);
        }
        catch (Exception e)
        {
            return new Response<Employee>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteEmployeeAsync(Guid employeeId)
    {
        try
        {
            var existingEmployee = await context.Employees.FindAsync(employeeId);
            if (existingEmployee == null)
            {
                return new Response<string>(HttpStatusCode.NotFound, "Not found the employee to delete");
            }

            context.Employees.Remove(existingEmployee);
            var result = await context.SaveChangesAsync();

            if (result == 0)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Failed to delete the employee");
            }
            
            return new Response<string>(HttpStatusCode.OK, "Deleted the employee successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<Employee>>> GetAllEmployeesAsync(JobPosition? position)
    {
        try
        {
            List<Employee> employees;

            if (position.HasValue)
                employees = await context.Employees.Where(e => e.Position == position).ToListAsync();
            
            else employees = await context.Employees.ToListAsync();

            if (!employees.Any())
            {
                return new Response<List<Employee>>(HttpStatusCode.NotFound, "Not found any employee");
            }
            
            return new Response<List<Employee>>(employees);
        }
        catch (Exception e)
        {
            return new Response<List<Employee>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}