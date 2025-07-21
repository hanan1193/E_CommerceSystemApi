using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        // Injection of the paymentMethod service to handle business logic.
        private readonly IPaymentMethodService _paymentMethodService;

        // Constructor injection for the paymentMethod service
        public PaymentMethodsController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentMethods()
        {
            var paymentMethods = await _paymentMethodService.GetPaymentMethods();
            return Ok(paymentMethods);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentMethod(int id)
        {
            var paymentMethod = await _paymentMethodService.GetPaymentMethodById(id);
            if (paymentMethod == null)
                return NotFound();

            return Ok(paymentMethod);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
           var createdPaymentMethod= await _paymentMethodService.AddPaymentMethod(paymentMethodViewModel);
            //return Ok("paymentMethod added successfully");
            return CreatedAtAction(nameof(GetPaymentMethod), new { id = createdPaymentMethod.PaymentMethodID }, createdPaymentMethod);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod(int id,[FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            if (id != paymentMethodViewModel.PaymentMethodID)
                return BadRequest("ID mismatch");
            var result = await _paymentMethodService.UpdatePaymentMethod(paymentMethodViewModel);
            if (!result)
                return NotFound("PaymentMethod not found");

            return Ok("PaymentMethod updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentMethod(int id)
        {
            var result = await _paymentMethodService.DeletePaymentMethod(id);
            if (!result)
                return NotFound("PaymentMethod not found");

            return Ok("PaymentMethod deleted successfully");
        }
    }
}

