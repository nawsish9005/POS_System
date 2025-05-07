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
    public class PurchaseController : ControllerBase
    {
        private readonly PosDbContext _context;
        public PurchaseController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/Purchase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetPurchases()
        {
            var purchases = await _context.Stocks
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseItems)
                .ThenInclude(pi => pi.Product)
                .ToListAsync();

            var purchaseDtos = purchases.Select(p => new StockDto
            {
                Id = p.Id,
                SupplierId = p.SupplierId,
                PurchaseDate = p.PurchaseDate,
                TotalAmount = p.TotalAmount,
                PurchaseItems = p.PurchaseItems.Select(pi => new PurchaseItemDto
                {
                    Id = pi.Id,
                    StockId = pi.StockId,
                    ProductId = pi.ProductId,
                    Quantity = pi.Quantity,
                    UnitCost = pi.UnitCost,
                    Subtotal = pi.Quantity * pi.UnitCost // Manually calculate Subtotal
                }).ToList()
            }).ToList();

            return Ok(purchaseDtos);
        }

        // GET: api/Purchase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetPurchaseById(int id)
        {
            var purchase = await _context.Stocks
                .Include(p => p.Supplier)
                .Include(p => p.PurchaseItems)
                .ThenInclude(pi => pi.Product)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }

            var purchaseDto = new StockDto
            {
                Id = purchase.Id,
                SupplierId = purchase.SupplierId,
                PurchaseDate = purchase.PurchaseDate,
                TotalAmount = purchase.TotalAmount,
                PurchaseItems = purchase.PurchaseItems.Select(pi => new PurchaseItemDto
                {
                    Id = pi.Id,
                    StockId = pi.StockId,
                    ProductId = pi.ProductId,
                    Quantity = pi.Quantity,
                    UnitCost = pi.UnitCost,
                    Subtotal = pi.Quantity * pi.UnitCost // Manually calculate Subtotal
                }).ToList()
            };

            return Ok(purchaseDto);
        }

        // POST: api/Purchase
        [HttpPost]
        public async Task<ActionResult<StockDto>> CreatePurchase(StockDto dto)
        {
            // Create the Purchase object
            var purchase = new Stock
            {
                SupplierId = dto.SupplierId,
                PurchaseDate = dto.PurchaseDate,
                TotalAmount = dto.TotalAmount,
                PurchaseItems = dto.PurchaseItems.Select(pi => new PurchaseItem
                {
                    ProductId = pi.ProductId,
                    Quantity = pi.Quantity,
                    UnitCost = pi.UnitCost,
                    Subtotal = pi.Quantity * pi.UnitCost // Calculate Subtotal
                }).ToList()
            };

            // Calculate TotalAmount based on the PurchaseItems
            purchase.TotalAmount = purchase.PurchaseItems.Sum(pi => pi.Subtotal);

            _context.Stocks.Add(purchase);
            await _context.SaveChangesAsync();

            // Map the created purchase back to DTO
            dto.Id = purchase.Id;
            return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.Id }, dto);
        }

        // PUT: api/Purchase/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, StockDto dto)
        {
            var purchase = await _context.Stocks
                .Include(p => p.PurchaseItems)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }

            purchase.SupplierId = dto.SupplierId;
            purchase.PurchaseDate = dto.PurchaseDate;
            purchase.PurchaseItems = dto.PurchaseItems.Select(pi => new PurchaseItem
            {
                ProductId = pi.ProductId,
                Quantity = pi.Quantity,
                UnitCost = pi.UnitCost,
                Subtotal = pi.Quantity * pi.UnitCost // Recalculate Subtotal
            }).ToList();

            // Recalculate TotalAmount
            purchase.TotalAmount = purchase.PurchaseItems.Sum(pi => pi.Subtotal);

            _context.Entry(purchase).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Purchase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Stocks
                .Include(p => p.PurchaseItems)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchase == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
