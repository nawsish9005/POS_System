using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.Data;
using POS_API.Dtos;
using POS_API.Models;

namespace POS_API.Controllers
{
    [Authorize("Roles =Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly PosDbContext _context;

        public CategoryController(PosDbContext context)
        {
            _context= context;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategory()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(new CategoryDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName
            });
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            dto.Id = category.Id; // return newly created Id

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, dto);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
                return NotFound();

            // Update only the fields you want to change
            existingCategory.CategoryName = dto.CategoryName;

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
