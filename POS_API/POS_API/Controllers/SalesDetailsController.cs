using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.Data;
using POS_API.Models;

namespace POS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesDetailsController : Controller
    {
        private readonly PosDbContext _context;

        public SalesDetailsController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesDetails>>> GetSalesDetails()
        {
            return await _context.SalesDetails
                .Include(sd => sd.Product)
                .Include(sd => sd.Sales)
                .ToListAsync();
        }

        // GET: api/SalesDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesDetails>> GetSalesDetail(int id)
        {
            var salesDetail = await _context.SalesDetails
                .Include(sd => sd.Product)
                .Include(sd => sd.Sales)
                .FirstOrDefaultAsync(sd => sd.Id == id);

            if (salesDetail == null)
            {
                return NotFound();
            }

            return salesDetail;
        }

        // POST: api/SalesDetails
        [HttpPost]
        public async Task<ActionResult<SalesDetails>> PostSalesDetail(SalesDetails salesDetail)
        {
            _context.SalesDetails.Add(salesDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSalesDetail), new { id = salesDetail.Id }, salesDetail);
        }

        // PUT: api/SalesDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesDetail(int id, SalesDetails salesDetail)
        {
            if (id != salesDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/SalesDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesDetail(int id)
        {
            var salesDetail = await _context.SalesDetails.FindAsync(id);
            if (salesDetail == null)
            {
                return NotFound();
            }

            _context.SalesDetails.Remove(salesDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesDetailExists(int id)
        {
            return _context.SalesDetails.Any(e => e.Id == id);
        }
    }
}
