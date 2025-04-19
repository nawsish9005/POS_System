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
    public class PurchaseItemController : ControllerBase
    {
        private readonly PosDbContext _context;
        public PurchaseItemController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseItemDto>>> GetPurchaseItems()
        {
            var purchaseItems = await _context.PurchaseItems
                .Include(pi => pi.Purchase)
                .Include(pi => pi.Product)
                .ToListAsync();

            // Manually calculate Subtotal when fetching data
            var purchaseItemsDto = purchaseItems.Select(pi => new PurchaseItemDto
            {
                Id = pi.Id,
                PurchaseId = pi.PurchaseId,
                ProductId = pi.ProductId,
                Quantity = pi.Quantity,
                UnitCost = pi.UnitCost,
                Subtotal = pi.Quantity * pi.UnitCost // Manually calculate Subtotal
            }).ToList();

            return Ok(purchaseItemsDto);
        }

        // GET: api/PurchaseItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseItemDto>> GetPurchaseItem(int id)
        {
            var purchaseItem = await _context.PurchaseItems
                .Include(pi => pi.Purchase)
                .Include(pi => pi.Product)
                .FirstOrDefaultAsync(pi => pi.Id == id);

            if (purchaseItem == null)
            {
                return NotFound();
            }

            // Manually calculate Subtotal when fetching data
            var purchaseItemDto = new PurchaseItemDto
            {
                Id = purchaseItem.Id,
                PurchaseId = purchaseItem.PurchaseId,
                ProductId = purchaseItem.ProductId,
                Quantity = purchaseItem.Quantity,
                UnitCost = purchaseItem.UnitCost,
                Subtotal = purchaseItem.Quantity * purchaseItem.UnitCost // Manually calculate Subtotal
            };

            return Ok(purchaseItemDto);
        }

        // POST: api/PurchaseItem
        [HttpPost]
        public async Task<ActionResult<PurchaseItemDto>> CreatePurchaseItem(PurchaseItemDto dto)
        {
            var purchaseItem = new PurchaseItem
            {
                PurchaseId = dto.PurchaseId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UnitCost = dto.UnitCost,
                Subtotal = dto.Quantity * dto.UnitCost // Calculate Subtotal and store it in the DB
            };

            _context.PurchaseItems.Add(purchaseItem);
            await _context.SaveChangesAsync();

            dto.Id = purchaseItem.Id;
            return CreatedAtAction(nameof(GetPurchaseItem), new { id = purchaseItem.Id }, dto);
        }

        // PUT: api/PurchaseItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchaseItem(int id, PurchaseItemDto dto)
        {
            var purchaseItem = await _context.PurchaseItems.FindAsync(id);

            if (purchaseItem == null)
            {
                return NotFound();
            }

            purchaseItem.PurchaseId = dto.PurchaseId;
            purchaseItem.ProductId = dto.ProductId;
            purchaseItem.Quantity = dto.Quantity;
            purchaseItem.UnitCost = dto.UnitCost;
            purchaseItem.Subtotal = dto.Quantity * dto.UnitCost; // Recalculate Subtotal

            _context.Entry(purchaseItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PurchaseItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseItem(int id)
        {
            var purchaseItem = await _context.PurchaseItems.FindAsync(id);

            if (purchaseItem == null)
            {
                return NotFound();
            }

            _context.PurchaseItems.Remove(purchaseItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
