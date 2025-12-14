using CffHackathon.Application.Common.Models.Order;
using CffHackathon.Application.Common.Models.Response;
using CffHackathon.Application.Common.Services;
using CffHackathon.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CffHackathon.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(Response<List<OrderReturnDto>>.Success(orders, 200));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderAsync(id);
        return Ok(Response<OrderReturnDto>.Success(order, 200));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreatedOrderDto createDto)
    {
        var orderId = await _orderService.AddOrderAsync(createDto);
        return Ok(Response<int>.Success(orderId, 201));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveOrder(int id)
    {
        await _orderService.RemoveOrderAsync(id);
        return Ok(Response<string>.Success("Order deleted successfully", 200));
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
    {
        await _orderService.UpdateOrderStatusAsync(id, status);
        return Ok(Response<string>.Success("Order status updated successfully", 200));
    }
}
