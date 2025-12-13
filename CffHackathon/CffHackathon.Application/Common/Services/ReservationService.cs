using CffHackathon.Application.Common.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Services
{
    public class ReservationService
    {
        private readonly IApplicationDbContext _context;

        public ReservationService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationInfoDto> ReserveAsync(ReservationAddDto dto, string customerId)
        {
            var start = dto.StartDateTime;
            var end = start.AddMinutes(30);

            bool exists = await _context.Reservations.AnyAsync(r =>
                r.TableId == dto.TableId &&
                start < r.EndDateTime &&
                end > r.StartDateTime);

            if (exists)
                throw new Exception("Bu vaxt üçün masa doludur");

            var reservation = new Reservation
            {
                TableId = dto.TableId,
                CustomerId = customerId,
                StartDateTime = start,
                EndDateTime = end
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ReservationInfoDto
            {
                Id = reservation.Id,
                TableId = reservation.TableId,
                StartDateTime = reservation.StartDateTime,
                EndDateTime = reservation.EndDateTime
            };
        }

        public async Task<List<ReservationInfoDto>> GetReservationsByCustomer(string customerId)
        {
            return await _context.Reservations
                .Where(r => r.CustomerId == customerId)
                .Select(r => new ReservationInfoDto
                {
                    Id = r.Id,
                    TableId = r.TableId,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime
                }).ToListAsync();
        }
    }
}

