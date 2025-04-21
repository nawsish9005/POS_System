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
    public class CustomerController : ControllerBase
    {
        private readonly PosDbContext _context;
        public CustomerController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomer()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Address = c.Address,
                    Phone = c.Phone,
                    Email = c.Email
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            var dto = new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email
            };

            return Ok(dto);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            dto.Id = customer.Id;
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, dto);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            customer.FullName = dto.FullName;
            customer.Address = dto.Address;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer deleted successfully." });
        }
    }
}
