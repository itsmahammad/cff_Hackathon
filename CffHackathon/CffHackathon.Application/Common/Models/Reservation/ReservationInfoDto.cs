using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Reservation
{
    public class ReservationInfoDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Status { get; set; } = null!;

    }
}
