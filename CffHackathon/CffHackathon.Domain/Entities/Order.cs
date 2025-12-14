using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Entities
{
    public class Order:BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public OrderStatus  Status { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string CustomerId { get; set; }
        public AppUser Customer { get; set; }
    }
}
