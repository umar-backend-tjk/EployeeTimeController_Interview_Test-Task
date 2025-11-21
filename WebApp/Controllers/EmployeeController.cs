using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto model)
    {
        var result= await employeeService.CreateEmployeeAsync(model);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto model)
    {
        var result= await employeeService.UpdateEmployeeAsync(model);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(Guid employeeId)
    {
        var result= await employeeService.DeleteEmployeeAsync(employeeId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployees([FromQuery] JobPosition? position)
    {
        var result= await employeeService.GetAllEmployeesAsync(position);
        return StatusCode(result.StatusCode, result);
    }
}