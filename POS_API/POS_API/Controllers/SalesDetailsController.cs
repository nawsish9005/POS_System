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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesDetails>>> GetSalesDetails()
        {
            return await _context.SalesDetails
                .Include(sd => sd.Product)
                .Include(sd => sd.Sales)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesDetails>> GetSalesDetail(int id)
        {
            var detail = await _context.SalesDetails
                .Include(sd => sd.Product)
                .Include(sd => sd.Sales)
                .FirstOrDefaultAsync(sd => sd.Id == id);

            if (detail == null)
                return NotFound();

            return detail;
        }

        [HttpPost]
        public async Task<ActionResult<SalesDetails>> CreateSalesDetail(SalesDetails salesDetail)
        {
            _context.SalesDetails.Add(salesDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSalesDetail), new { id = salesDetail.Id }, salesDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesDetail(int id, SalesDetails salesDetail)
        {
            if (id != salesDetail.Id)
                return BadRequest();

            _context.Entry(salesDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesDetail(int id)
        {
            var detail = await _context.SalesDetails.FindAsync(id);
            if (detail == null)
                return NotFound();

            _context.SalesDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
