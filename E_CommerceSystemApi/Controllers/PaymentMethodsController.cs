using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystemApi.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            await _paymentMethodService.AddPaymentMethod(paymentMethodViewModel);
            //return Ok("paymentMethod added successfully");
            return CreatedAtAction(nameof(GetPaymentMethod), new { id = paymentMethodViewModel.PaymentMethodID }, paymentMethodViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePaymentMethod([FromBody] PaymentMethodViewModel model)
        {
            var result = await _paymentMethodService.UpdatePaymentMethod(model);
            if (!result)
                return NotFound("PaymentMethod not found");

            return Ok("PaymentMethod updated successfully");
        }

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

