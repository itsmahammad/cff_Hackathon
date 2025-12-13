using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Domain.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }

        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; } = true;
        public Category Category { get; set; } = null!;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
