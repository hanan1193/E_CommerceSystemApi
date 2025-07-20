using E_CommerceSystemApi.BLL.Services.impl;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // Injection of the category service to handle business logic.
        private readonly IOrderService _orderService;

        // Constructor injection for the order service
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderViewModel orderViewModelmodel)
        {
            var createdOrder = await _orderService.AddOrder(orderViewModelmodel);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderID }, createdOrder);
        }
        // api/Categories/{id} 
        // Endpoint to update a Order by ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderViewModel orderViewModelmodel)
        {
            if (id != orderViewModelmodel.OrderID)
                return BadRequest("ID mismatch");
            var result = await _orderService.UpdateOrder(orderViewModelmodel);
            if (!result)
                return NotFound("Order not found");

            return Ok("Order updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (!result)
                return NotFound("Order not found");

            return Ok("Order deleted successfully");
        }
    }
}

