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
                .Include(s => s.Payments)
                .ToListAsync();

            var result = sales.Select(sale => new SaleDto
            {
                Id = sale.Id,
                SaleDate = sale.SaleDate,
                CustomerId = sale.CustomerId,
                TotalAmount = sale.TotalAmount,
                PaidAmount = sale.PaidAmount,
                ChangeAmount = sale.ChangeAmount,
                UserId = sale.UserId,
                SaleItems = sale.SaleItems.Select(item => new SaleItemDto
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                Payments = sale.Payments.Select(p => new PaymentDto
                {
                    Id = p.Id,
                    SaleId = p.SaleId,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod
                }).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetById(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .Include(s => s.Payments)
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
                SaleItems = sale.SaleItems.Select(item => new SaleItemDto
                {
                    Id = item.Id,
                    SaleId = item.SaleId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                Payments = sale.Payments.Select(p => new PaymentDto
                {
                    Id = p.Id,
                    SaleId = p.SaleId,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SaleDto dto)
        {
            var sale = new Sale
            {
                SaleDate = dto.SaleDate,
                CustomerId = dto.CustomerId,
                TotalAmount = dto.TotalAmount,
                PaidAmount = dto.PaidAmount,
                ChangeAmount = dto.ChangeAmount,
                UserId = dto.UserId,
                SaleItems = dto.SaleItems.Select(item => new SaleItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                Payments = dto.Payments.Select(p => new Payment
                {
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod
                }).ToList()
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return Ok(new { sale.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SaleDto dto)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            sale.SaleDate = dto.SaleDate;
            sale.CustomerId = dto.CustomerId;
            sale.TotalAmount = dto.TotalAmount;
            sale.PaidAmount = dto.PaidAmount;
            sale.ChangeAmount = dto.ChangeAmount;
            sale.UserId = dto.UserId;

            // Update SaleItems
            _context.SaleItems.RemoveRange(sale.SaleItems);
            sale.SaleItems = dto.SaleItems.Select(item => new SaleItem
            {
                SaleId = id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList();

            // Update Payments
            _context.Payments.RemoveRange(sale.Payments);
            sale.Payments = dto.Payments.Select(p => new Payment
            {
                SaleId = id,
                Amount = p.Amount,
                PaymentMethod = p.PaymentMethod
            }).ToList();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.SaleItems)
                .Include(s => s.Payments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            _context.SaleItems.RemoveRange(sale.SaleItems);
            _context.Payments.RemoveRange(sale.Payments);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
