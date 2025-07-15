using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_CommerceSystemApi.DAL.Data;
using E_CommerceSystemApi.DAL.Models;
using E_CommerceSystemApi.BLL.Services.intf;
using E_CommerceSystemApi.BLL.ViewModels;


namespace E_CommerceSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Injection of the category service to handle business logic.
        private readonly ICategoryService _categoryService;

        // Constructor injection for the category service
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryViewModel model)
        {
            await _categoryService.AddCategory(model);
            //return Ok("Category added successfully");
            return CreatedAtAction(nameof(GetCategory), new { id = model.Id }, model);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryViewModel model)
        {
            var result = await _categoryService.UpdateCategory(model);
            if (!result)
                return NotFound("Category not found");

            return Ok("Category updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
                return NotFound("Category not found");

            return Ok("Category deleted successfully");
        }
    }
}
    

