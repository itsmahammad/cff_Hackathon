using CffHackathon.Application.Common.Models.Reservation;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using CffHackathon.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(Response<List<ReservationInfoDto>>.Success(reservations, 200));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        return Ok(Response<ReservationInfoDto>.Success(reservation, 200));
    }

    [HttpGet("table/{tableId:int}")]
    public async Task<IActionResult> GetReservationsByTableId(int tableId)
    {
        var reservations = await _reservationService.GetReservationsByTableIdAsync(tableId);
        return Ok(Response<List<ReservationInfoDto>>.Success(reservations, 200));
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationAddDto createDto)
    {
        var reservationId = await _reservationService.CreateReservationAsync(createDto);
        return Ok(Response<int>.Success(reservationId, 201));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveReservation(int id)
    {
        await _reservationService.RemoveReservationAsync(id);
        return Ok(Response<string>.Success("Reservation deleted successfully", 200));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateReservationStatus(int id, [FromBody] ReservationStatus status)
    {
        await _reservationService.UpdateReservationStatusAsync(id, status);
        return Ok(Response<string>.Success("Reservation status updated successfully", 200));
    }
}
