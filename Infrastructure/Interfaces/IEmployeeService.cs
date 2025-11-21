using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IEmployeeService
{
    Task<Response<Employee>> CreateEmployeeAsync(CreateEmployeeDto model);
    Task<Response<Employee>> UpdateEmployeeAsync(UpdateEmployeeDto model);
    Task<Response<string>> DeleteEmployeeAsync(Guid employeeId);
    Task<Response<List<Employee>>> GetAllEmployeesAsync(JobPosition? position);
}