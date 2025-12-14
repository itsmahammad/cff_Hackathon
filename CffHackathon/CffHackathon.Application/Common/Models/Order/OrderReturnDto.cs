using CffHackathon.Application.Common.Models.OrderItem;
using CffHackathon.Domain.Enums;

namespace CffHackathon.Application.Common.Models.Order
{
    public class OrderReturnDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TableNumber { get; set; }
        public int TableId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemReturnDto> OrderItems { get; set; }
    }
}
