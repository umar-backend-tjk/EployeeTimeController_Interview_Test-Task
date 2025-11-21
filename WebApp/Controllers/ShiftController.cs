using Domain.DTOs.Shift;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftController(IShiftService shiftService) : ControllerBase
{
    [HttpPost("start-shift")]
    public async Task<IActionResult> StartShift(ShiftDto requestDto)
    {
        var result = await shiftService.StartShift(requestDto);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("end-shift")]
    public async Task<IActionResult> EndShift(ShiftDto requestDto)
    {
        var result = await shiftService.EndShift(requestDto);
        return StatusCode(result.StatusCode, result);
    }
}