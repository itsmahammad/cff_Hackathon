using CffHackathon.Domain.Enums;

namespace CffHackathon.Application.Common.Models.Reservation
{
    public class ReservationInfoDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
