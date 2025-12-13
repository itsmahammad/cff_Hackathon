using CffHackathon.Application.Common.Models.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Order
{
    public  class CreatedOrderDto
    {
        
        public int TableId { get; set; }
        public string CustomerId { get; set; }
        public List<CreatedOrderItemDto> Items { get; set; }
    }
}
