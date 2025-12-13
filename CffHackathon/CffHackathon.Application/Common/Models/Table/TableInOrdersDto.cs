using CffHackathon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Table
{
    public class TableInOrdersDto
    {
        public string Number { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
    }
}
