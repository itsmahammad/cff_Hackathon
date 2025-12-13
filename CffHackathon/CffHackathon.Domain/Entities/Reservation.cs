using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Entities
{
    public class Reservation:BaseEntity
    {
        //public int CustomerId { get; set; }
        //public User Customer { get; set; }

        public int TableId { get; set; }
        public Table Table { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public ReservationStatus Status { get; set; }
    }
}
