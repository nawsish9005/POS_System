using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_API.Data;
using POS_API.Dtos;
using POS_API.Models;

namespace POS_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PosDbContext _context;
        public PaymentController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll()
        {
            var payments = await _context.Payments
                .Include(p => p.Sale)  // Include the related Sale data if needed
                .ToListAsync();

            var result = payments.Select(payment => new PaymentDto
            {
                Id = payment.Id,
                SaleId = payment.SaleId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod
            }).ToList();

            return Ok(result);
        }

        // GET: api/payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetById(int id)
        {
            var payment = await _context.Payments
                .Include(p => p.Sale) // Include the related Sale data if needed
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
                return NotFound();

            var result = new PaymentDto
            {
                Id = payment.Id,
                SaleId = payment.SaleId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod
            };

            return Ok(result);
        }

        // POST: api/payment
        [HttpPost]
        public async Task<ActionResult> Create(PaymentDto dto)
        {
            var payment = new Payment
            {
                SaleId = dto.SaleId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, new PaymentDto
            {
                Id = payment.Id,
                SaleId = payment.SaleId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod
            });
        }

        // PUT: api/payment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PaymentDto dto)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
                return NotFound();

            payment.SaleId = dto.SaleId;
            payment.Amount = dto.Amount;
            payment.PaymentMethod = dto.PaymentMethod;

            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/payment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
