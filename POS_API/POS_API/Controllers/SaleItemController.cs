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
    public class SaleItemController : ControllerBase
    {
        private readonly PosDbContext _context;

        public SaleItemController(PosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleItemDto>>> GetAll()
        {
            var items = await _context.SaleItems
                .Select(i => new SaleItemDto
                {
                    Id = i.Id,
                    SaleId = i.SaleId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleItemDto>> GetById(int id)
        {
            var item = await _context.SaleItems.FindAsync(id);

            if (item == null)
                return NotFound();

            var dto = new SaleItemDto
            {
                Id = item.Id,
                SaleId = item.SaleId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleItemDto dto)
        {
            var item = new SaleItem
            {
                SaleId = dto.SaleId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice
            };

            _context.SaleItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale item created successfully", item.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SaleItemDto dto)
        {
            var item = await _context.SaleItems.FindAsync(id);

            if (item == null)
                return NotFound();

            item.SaleId = dto.SaleId;
            item.ProductId = dto.ProductId;
            item.Quantity = dto.Quantity;
            item.UnitPrice = dto.UnitPrice;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale item updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.SaleItems.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.SaleItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale item deleted successfully" });
        }
    }
}
