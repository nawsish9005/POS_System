using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.Data;
using POS_API.Models;

namespace POS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesProductController : Controller
    {
        private readonly PosDbContext _context;

        public SalesProductController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesProduct>>> GetSalesProducts()
        {
            return await _context.SalesProducts
                .Include(sp => sp.Sales)
                .Include(sp => sp.Product)
                .ToListAsync();
        }

        // GET: api/SalesProduct/{salesId}/{productId}
        [HttpGet("{salesId}/{productId}")]
        public async Task<ActionResult<SalesProduct>> GetSalesProduct(int salesId, int productId)
        {
            var salesProduct = await _context.SalesProducts
                .Include(sp => sp.Sales)
                .Include(sp => sp.Product)
                .FirstOrDefaultAsync(sp => sp.SalesId == salesId && sp.ProductId == productId);

            if (salesProduct == null)
            {
                return NotFound();
            }

            return salesProduct;
        }

        // POST: api/SalesProduct
        [HttpPost]
        public async Task<ActionResult<SalesProduct>> PostSalesProduct(SalesProduct salesProduct)
        {
            _context.SalesProducts.Add(salesProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesProduct),
                new { salesId = salesProduct.SalesId, productId = salesProduct.ProductId },
                salesProduct);
        }

        // DELETE: api/SalesProduct/{salesId}/{productId}
        [HttpDelete("{salesId}/{productId}")]
        public async Task<IActionResult> DeleteSalesProduct(int salesId, int productId)
        {
            var salesProduct = await _context.SalesProducts
                .FirstOrDefaultAsync(sp => sp.SalesId == salesId && sp.ProductId == productId);

            if (salesProduct == null)
            {
                return NotFound();
            }

            _context.SalesProducts.Remove(salesProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesProductExists(int salesId, int productId)
        {
            return _context.SalesProducts.Any(e => e.SalesId == salesId && e.ProductId == productId);
        }

    }
}
