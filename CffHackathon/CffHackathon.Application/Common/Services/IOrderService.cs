using CffHackathon.Application.Common.Models.Category;
using CffHackathon.Application.Common.Models.Order;
using CffHackathon.Application.Common.Models.Table;
using CffHackathon.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(CreatedOrderDto createDto);
        Task<List<OrderReturnDto>> GetAllOrdersAsync();
        Task RemoveOrderAsync(int orderId);
        Task<OrderReturnDto> GetOrderAsync(int orderId);
    }
    public class OrderService(IApplicationDbContext dbContext) : IOrderService
    {
        public async Task AddOrderAsync(CreatedOrderDto createDto)
        {
            Order order = new Order
            {
                TableId = createDto.TableId
            };
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            var  orders = await dbContext.Orders
               .Select(o => new OrderReturnDto
               {
                   TableName = o.Table.Number,
                   Status = o.Status.ToString(),
                   TotalPrice = o.OrderItems.Sum(x => x.Price * x.Quantity)
                       
                       
               })
               .ToListAsync();
            return orders;
        }

        public async Task<OrderReturnDto> GetOrderAsync(int orderId)
        {
            var order = await dbContext.Orders
                .Where(x => x.Id == orderId)
              .Select(o => new OrderReturnDto
              {
                  TableName = o.Table.Number,
                  Status = o.Status.ToString(),
                  TotalPrice = o.OrderItems.Sum(x => x.Price * x.Quantity)
                  
                      
              }).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new NotFoundException("Order not found");
            }
            return order;
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException("Category not found");
            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();

        }
    }
}
