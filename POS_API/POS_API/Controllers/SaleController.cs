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
    public class SaleController : ControllerBase
    {
        private readonly PosDbContext _context;

        public SaleController(PosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
        {
            var sales = await _context.Sales
                .Include(s => s.SaleItems)
                .Select(s => new SaleDto
                {
                    Id = s.Id,
                    SaleDate = s.SaleDate,
                    CustomerId = s.CustomerId,
                    TotalAmount = s.TotalAmount,
                    PaidAmount = s.PaidAmount,
                    ChangeAmount = s.ChangeAmount,
                    UserId = s.UserId,
                    SaleItems = s.SaleItems.Select(i => new SaleItemDto
                    {
                        Id = i.Id,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetById(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            var dto = new SaleDto
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                TotalAmount = sale.TotalAmount,
                PaidAmount = sale.PaidAmount,
                ChangeAmount = sale.ChangeAmount,
                UserId = sale.UserId,
                SaleItems = sale.SaleItems.Select(i => new SaleItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleDto dto)
        {
            var sale = new Sale
            {
                SaleDate = dto.SaleDate,
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                PaidAmount = dto.PaidAmount,
                ChangeAmount = dto.ChangeAmount,
                UserId = dto.UserId,
                SaleItems = dto.SaleItems.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale created successfully", sale.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SaleDto dto)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            // Update sale properties
            sale.SaleDate = dto.SaleDate;
            sale.CustomerId = dto.CustomerId;
            sale.TotalAmount = dto.TotalAmount;
            sale.PaidAmount = dto.PaidAmount;
            sale.ChangeAmount = dto.ChangeAmount;
            sale.UserId = dto.UserId;

            // Clear existing items and re-add
            _context.SaleItems.RemoveRange(sale.SaleItems);

            sale.SaleItems = dto.SaleItems.Select(i => new SaleItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            _context.SaleItems.RemoveRange(sale.SaleItems);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Sale deleted successfully" });
        }
    }
}
