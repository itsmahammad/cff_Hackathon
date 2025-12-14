using CffHackathon.Application.Common.Interfaces;
using CffHackathon.Application.Common.Models.Reservation;
using CffHackathon.Domain.Entities;
using CffHackathon.Domain.Enums;
using CffHackathon.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CffHackathon.Application.Common.Services
{
    public interface IReservationService
    {
        Task<int> CreateReservationAsync(ReservationAddDto createDto);
        Task<List<ReservationInfoDto>> GetAllReservationsAsync();
        Task<ReservationInfoDto> GetReservationByIdAsync(int id);
        Task RemoveReservationAsync(int id);
        Task UpdateReservationStatusAsync(int id, ReservationStatus status);
        Task<List<ReservationInfoDto>> GetReservationsByTableIdAsync(int tableId);
    }

    public class ReservationService(IApplicationDbContext dbContext) : IReservationService
    {
        private const int MinimumReservationMinutes = 30;

        public async Task<int> CreateReservationAsync(ReservationAddDto createDto)
        {
            var tableExists = await dbContext.Tables.AnyAsync(t => t.Id == createDto.TableId);
            if (!tableExists)
            {
                throw new NotFoundException($"Table with id {createDto.TableId} not found.");
            }

            var duration = createDto.EndDateTime - createDto.StartDateTime;
            if (duration.TotalMinutes < MinimumReservationMinutes)
            {
                throw new BadRequestException($"Reservation duration must be at least {MinimumReservationMinutes} minutes.");
            }

            if (createDto.StartDateTime < DateTime.UtcNow)
            {
                throw new BadRequestException("Reservation start time cannot be in the past.");
            }

            if (createDto.EndDateTime <= createDto.StartDateTime)
            {
                throw new BadRequestException("Reservation end time must be after start time.");
            }

            var hasConflict = await dbContext.Reservations
                .Where(r => r.TableId == createDto.TableId && 
                           r.Status != ReservationStatus.Cancelled)
                .AnyAsync(r => 
                    (createDto.StartDateTime >= r.StartDateTime && createDto.StartDateTime < r.EndDateTime) ||
                    (createDto.EndDateTime > r.StartDateTime && createDto.EndDateTime <= r.EndDateTime) ||
                    (createDto.StartDateTime <= r.StartDateTime && createDto.EndDateTime >= r.EndDateTime)
                );

            if (hasConflict)
            {
                throw new BadRequestException("This table is already reserved for the selected time period.");
            }

            var reservation = new Reservation
            {
                CustomerId = createDto.CustomerId,
                TableId = createDto.TableId,
                StartDateTime = createDto.StartDateTime,
                EndDateTime = createDto.EndDateTime,
                Status = ReservationStatus.Pending
            };

            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<List<ReservationInfoDto>> GetAllReservationsAsync()
        {
            var reservations = await dbContext.Reservations
                .Select(r => new ReservationInfoDto
                {
                    Id = r.Id,
                    CustomerId = r.CustomerId,
                    TableId = r.TableId,
                    TableNumber = r.Table.Number,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime,
                    Status = r.Status
                })
                .ToListAsync();

            return reservations;
        }

        public async Task<ReservationInfoDto> GetReservationByIdAsync(int id)
        {
            var reservation = await dbContext.Reservations
                .Where(r => r.Id == id)
                .Select(r => new ReservationInfoDto
                {
                    Id = r.Id,
                    CustomerId = r.CustomerId,
                    TableId = r.TableId,
                    TableNumber = r.Table.Number,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime,
                    Status = r.Status
                })
                .FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with id {id} not found.");
            }

            return reservation;
        }

        public async Task<List<ReservationInfoDto>> GetReservationsByTableIdAsync(int tableId)
        {
            var reservations = await dbContext.Reservations
                .Where(r => r.TableId == tableId)
                .Select(r => new ReservationInfoDto
                {
                    Id = r.Id,
                    CustomerId = r.CustomerId,
                    TableId = r.TableId,
                    TableNumber = r.Table.Number,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime,
                    Status = r.Status
                })
                .ToListAsync();

            return reservations;
        }

        public async Task RemoveReservationAsync(int id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with id {id} not found.");
            }

            dbContext.Reservations.Remove(reservation);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateReservationStatusAsync(int id, ReservationStatus status)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new NotFoundException($"Reservation with id {id} not found.");
            }

            reservation.Status = status;
            await dbContext.SaveChangesAsync();
        }
    }
}
