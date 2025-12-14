namespace CffHackathon.Application.Common.Models.Reservation
{
    public class ReservationAddDto
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
