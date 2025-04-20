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
    public class TaxController : ControllerBase
    {
        private readonly PosDbContext _context;
        public TaxController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/Tax
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxDto>>> GetTaxes()
        {
            var taxes = await _context.Taxes
                .Select(t => new TaxDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Rate = t.Rate
                })
                .ToListAsync();

            return Ok(taxes);
        }

        // GET: api/Tax/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxDto>> GetTax(int id)
        {
            var tax = await _context.Taxes.FindAsync(id);

            if (tax == null)
                return NotFound();

            return new TaxDto
            {
                Id = tax.Id,
                Name = tax.Name,
                Rate = tax.Rate
            };
        }

        // POST: api/Tax
        [HttpPost]
        public async Task<ActionResult<TaxDto>> CreateTax(TaxDto dto)
        {
            var tax = new Tax
            {
                Name = dto.Name,
                Rate = dto.Rate
            };

            _context.Taxes.Add(tax);
            await _context.SaveChangesAsync();

            dto.Id = tax.Id;

            return CreatedAtAction(nameof(GetTax), new { id = tax.Id }, dto);
        }

        // PUT: api/Tax/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTax(int id, TaxDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var tax = await _context.Taxes.FindAsync(id);
            if (tax == null)
                return NotFound();

            tax.Name = dto.Name;
            tax.Rate = dto.Rate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Tax/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTax(int id)
        {
            var tax = await _context.Taxes.FindAsync(id);
            if (tax == null)
                return NotFound();

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
