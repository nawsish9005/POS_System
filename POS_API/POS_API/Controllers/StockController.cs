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
    public class StockController : ControllerBase
    {
        private readonly PosDbContext _context;
        public StockController(PosDbContext context)
        {
            _context = context;
        }


        // GET: api/Stock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStocks()
        {
            var stocks = await _context.Stocks
                .Include(s => s.Supplier)
                .Include(s => s.Product)
                .Include(s => s.Branches)
                .ToListAsync();

            var result = stocks.Select(s => new StockDto
            {
                Id = s.Id,
                SupplierId = s.SupplierId,
                ProductId = s.ProductId,
                Quantity = s.Quantity,
                BranchesId = s.BranchesId,
                PurchaseDate = s.PurchaseDate,
                TotalAmount = s.TotalAmount
            });

            return Ok(result);
        }

        // GET: api/Stock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockDto>> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return NotFound();

            var dto = new StockDto
            {
                Id = stock.Id,
                SupplierId = stock.SupplierId,
                ProductId = stock.ProductId,
                Quantity = stock.Quantity,
                BranchesId = stock.BranchesId,
                PurchaseDate = stock.PurchaseDate,
                TotalAmount = stock.TotalAmount
            };

            return Ok(dto);
        }

        // POST: api/Stock
        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock(StockDto stockDto)
        {
            var stock = new Stock
            {
                SupplierId = stockDto.SupplierId,
                ProductId = stockDto.ProductId,
                Quantity = stockDto.Quantity,
                BranchesId = stockDto.BranchesId,
                PurchaseDate = stockDto.PurchaseDate,
                TotalAmount = stockDto.TotalAmount
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            stockDto.Id = stock.Id;

            return CreatedAtAction(nameof(GetStock), new { id = stock.Id }, stockDto);
        }

        // PUT: api/Stock/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock(int id, StockDto stockDto)
        {
            if (id != stockDto.Id)
                return BadRequest();

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound();

            stock.SupplierId = stockDto.SupplierId;
            stock.ProductId = stockDto.ProductId;
            stock.Quantity = stockDto.Quantity;
            stock.BranchesId = stockDto.BranchesId;
            stock.PurchaseDate = stockDto.PurchaseDate;
            stock.TotalAmount = stockDto.TotalAmount;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Stock/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
                return NotFound();

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}

