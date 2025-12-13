using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Entities
{
    public class Table:BaseEntity
    {
        public string Number { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
    }
}
