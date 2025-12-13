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
        private readonly ReservationService _reservationService;
        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(ReservationAddDto dto, string customerId)
        {
            var result = await _reservationService.ReserveAsync(dto, customerId);
            var response = Response<ReservationInfoDto>.Success(result, 200);
            return Ok(response);
        }
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetReservationsByCustomer(string customerId)
        {
            var response = Response<List<ReservationInfoDto>>.Success(await _reservationService.GetReservationsByCustomer(customerId), 200);
            return Ok(response);
        }
    }
}