using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Reservation
{
    public class ReservationAddDto
    {
        public int TableId { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}
