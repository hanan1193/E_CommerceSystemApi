using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;
using E_CommerceSystemApi.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Injection of the product service to handle business logic.
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        //api/roles/
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }
        // api/products/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Getproduct(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        // Endpoint restricted to Admin role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductViewModel productViewModel)
        {
            if (productViewModel == null)
            {
                return BadRequest("product data is required.");
            }
           var createdProduct= await _productService.AddProduct(productViewModel);
            return CreatedAtAction(nameof(Getproduct), new { id = createdProduct.ProductID }, createdProduct);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Updateproduct(int id,[FromBody] ProductViewModel product)
        {
            if(id != product.ProductID)
            {
                return BadRequest("ID mismatch. Please provide the correct ID.");
            }
            if (product == null)
            {
                return BadRequest("product data is required.");
            }
            await _productService.UpdateProduct(product);
            return Ok("Product updated successfully");
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var result = await _productService.GetProductById(id);
            if (result == null)
            {
                return NotFound($"product with ID {id} not found.");
            }
            else
            {
                return Ok("Product deleted successfully");
            }

        }

    }
}
