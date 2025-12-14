using CffHackathon.Application.Common.Models.Reservation;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CffHackathon.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(ReservationAddDto dto)
        {
            var result = await _reservationService.CreateReservationAsync(dto);
            var response = Response<int>.Success(result, 200);
            return Ok(response);
        }
        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetReservationsByTable(string tableId)
        {
            var result = await _reservationService.GetReservationsByTableIdAsync(int.Parse(tableId));
            var response = Response<List<ReservationInfoDto>>.Success(result, 200);
            return Ok(response);
        }
    }
}