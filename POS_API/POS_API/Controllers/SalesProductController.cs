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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesProduct>>> GetSalesProducts()
        {
            return await _context.SalesProducts
                .Include(sp => sp.Sales)
                .Include(sp => sp.Product)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesProduct>> GetSalesProduct(int id)
        {
            var item = await _context.SalesProducts
                .Include(sp => sp.Sales)
                .Include(sp => sp.Product)
                .FirstOrDefaultAsync(sp => sp.Id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<SalesProduct>> CreateSalesProduct(SalesProduct salesProduct)
        {
            _context.SalesProducts.Add(salesProduct);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSalesProduct), new { id = salesProduct.Id }, salesProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesProduct(int id, SalesProduct salesProduct)
        {
            if (id != salesProduct.Id)
                return BadRequest();

            _context.Entry(salesProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesProduct(int id)
        {
            var item = await _context.SalesProducts.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.SalesProducts.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
