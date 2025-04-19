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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
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
        public async Task<ActionResult<CategoryDto>> GetById(int id)
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
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            dto.Id = category.Id; // return newly created Id

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, dto);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            category.CategoryName = dto.CategoryName;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
