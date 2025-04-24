using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.Data;
using POS_API.Dtos;
using POS_API.Models;

namespace POS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly PosDbContext _context;
        public DiscountController(PosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiscountDto>>> GetAllDiscount()
        {
            var discounts = await _context.Discounts
                .Select(d => new DiscountDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Percentage = d.Percentage,
                    IsActive = d.IsActive
                })
                .ToListAsync();

            return Ok(discounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDto>> GetDiscountById(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return NotFound();

            var dto = new DiscountDto
            {
                Id = discount.Id,
                Name = discount.Name,
                Percentage = discount.Percentage,
                IsActive = discount.IsActive
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiscount(DiscountDto dto)
        {
            var discount = new Discount
            {
                Name = dto.Name,
                Percentage = dto.Percentage,
                IsActive = dto.IsActive
            };

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            dto.Id = discount.Id;

            return CreatedAtAction(nameof(GetDiscountById), new { id = discount.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiscount(int id, DiscountDto dto)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return NotFound();

            discount.Name = dto.Name;
            discount.Percentage = dto.Percentage;
            discount.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscount(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null) return NotFound();

            _context.Discounts.Remove(discount);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
