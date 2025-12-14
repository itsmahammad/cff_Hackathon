using CffHackathon.Application.Common.Interfaces;
using CffHackathon.Application.Common.Models.Order;
using CffHackathon.Application.Common.Models.OrderItem;
using CffHackathon.Domain.Entities;
using CffHackathon.Domain.Enums;
using CffHackathon.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CffHackathon.Application.Common.Services
{
    public interface IOrderService
    {
        Task<int> AddOrderAsync(CreatedOrderDto createDto);
        Task<List<OrderReturnDto>> GetAllOrdersAsync();
        Task RemoveOrderAsync(int orderId);
        Task<OrderReturnDto> GetOrderAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }

    public class OrderService(IApplicationDbContext dbContext) : IOrderService
    {
        public async Task<int> AddOrderAsync(CreatedOrderDto createDto)
        {
            var tableExists = await dbContext.Tables.AnyAsync(t => t.Id == createDto.TableId);
            if (!tableExists)
            {
                throw new NotFoundException($"Table with id {createDto.TableId} not found.");
            }

            var order = new Order
            {
                TableId = createDto.TableId,
                CreatedDate = DateTime.UtcNow,
                Status = OrderStatus.Pending
            };

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            if (createDto.Items != null && createDto.Items.Any())
            {
                foreach (var item in createDto.Items)
                {
                    var menuItem = await dbContext.MenuItems.FindAsync(item.MenuItemId);
                    if (menuItem == null)
                    {
                        throw new NotFoundException($"MenuItem with id {item.MenuItemId} not found.");
                    }

                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        MenuItemId = item.MenuItemId,
                        Quantity = item.Quantity,
                        Price = menuItem.Price
                    };

                    await dbContext.OrderItems.AddAsync(orderItem);
                }
                await dbContext.SaveChangesAsync();
            }

            return order.Id;
        }

        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            var orders = await dbContext.Orders
                .Include(o => o.Table)
                .Select(o => new OrderReturnDto
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    TableNumber = o.Table.Number,
                    TableId = o.TableId,
                    Status = o.Status,
                    TotalPrice = dbContext.OrderItems
                        .Where(oi => oi.OrderId == o.Id)
                        .Sum(oi => oi.Price * oi.Quantity),
                    OrderItems = dbContext.OrderItems
                        .Where(oi => oi.OrderId == o.Id)
                        .Select(oi => new OrderItemReturnDto
                        {
                            Id = oi.Id,
                            MenuItemId = oi.MenuItemId,
                            MenuItemName = oi.MenuItem.Name,
                            Quantity = oi.Quantity,
                            Price = oi.Price,
                            Subtotal = oi.Price * oi.Quantity
                        })
                        .ToList()
                })
                .ToListAsync();

            return orders;
        }

        public async Task<OrderReturnDto> GetOrderAsync(int orderId)
        {
            var order = await dbContext.Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderReturnDto
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    TableNumber = o.Table.Number,
                    TableId = o.TableId,
                    Status = o.Status,
                    TotalPrice = dbContext.OrderItems
                        .Where(oi => oi.OrderId == o.Id)
                        .Sum(oi => oi.Price * oi.Quantity),
                    OrderItems = dbContext.OrderItems
                        .Where(oi => oi.OrderId == o.Id)
                        .Select(oi => new OrderItemReturnDto
                        {
                            Id = oi.Id,
                            MenuItemId = oi.MenuItemId,
                            MenuItemName = oi.MenuItem.Name,
                            Quantity = oi.Quantity,
                            Price = oi.Price,
                            Subtotal = oi.Price * oi.Quantity
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new NotFoundException($"Order with id {orderId} not found.");
            }

            return order;
        }

        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException($"Order with id {orderId} not found.");
            }

            var orderItems = await dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
            dbContext.OrderItems.RemoveRange(orderItems);
            dbContext.Orders.Remove(order);
            
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new NotFoundException($"Order with id {orderId} not found.");
            }

            order.Status = status;
            await dbContext.SaveChangesAsync();
        }
    }
}
