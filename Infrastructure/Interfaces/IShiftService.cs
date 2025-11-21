using Domain.DTOs.Shift;
using Domain.Entities;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IShiftService
{
    Task<Response<Shift>> StartShift(ShiftDto requestDto);
    Task<Response<Shift>> EndShift(ShiftDto requestDto);
}