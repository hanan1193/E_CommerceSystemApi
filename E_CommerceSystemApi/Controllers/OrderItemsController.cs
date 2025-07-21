using E_CommerceSystemApi.BLL.Services.impl;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        // Injection of the orderItem service to handle business logic.
        private readonly IOrderItemService _orderItemService;

        // Constructor injection for the orderItem service
        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderItems()
        {
            var orderItems = await _orderItemService.GetOrderItems();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(int id)
        {
            var orderItem = await _orderItemService.GetOrderItemById(id);
            if (orderItem == null)
                return NotFound();

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemViewModel orderItemViewModelmodel)
        {
            var createdOrderItem = await _orderItemService.AddOrderItem(orderItemViewModelmodel);
            return CreatedAtAction(nameof(GetOrderItem), new { id = createdOrderItem.OrderItemID }, createdOrderItem);
        }
        // api/orderItems/{id} 
        // Endpoint to update a orderItem by ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemViewModel orderItemViewModelmodel)
        {
            if (id != orderItemViewModelmodel.OrderItemID)
                return BadRequest("ID mismatch");
            var result = await _orderItemService.UpdateOrderItem(orderItemViewModelmodel);
            if (!result)
                return NotFound("orderItem not found");

            return Ok("orderItem updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemService.DeleteOrderItem(id);
            if (!result)
                return NotFound("orderItem not found");

            return Ok("orderItem deleted successfully");
        }
    }
}
