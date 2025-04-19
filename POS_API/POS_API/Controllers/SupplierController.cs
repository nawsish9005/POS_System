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
    public class SupplierController : ControllerBase
    {
        private readonly PosDbContext _context;
        public SupplierController(PosDbContext context)
        {
            _context = context;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new SupplierDto
                {
                    Id = s.Id,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    Phone = s.Phone,
                    Email = s.Email,
                    Address = s.Address
                }).ToListAsync();

            return Ok(suppliers);
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetById(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            var dto = new SupplierDto
            {
                Id = supplier.Id,
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                Phone = supplier.Phone,
                Email = supplier.Email,
                Address = supplier.Address
            };

            return Ok(dto);
        }

        // POST: api/Supplier
        [HttpPost]
        public async Task<IActionResult> Create(SupplierDto dto)
        {
            var supplier = new Supplier
            {
                CompanyName = dto.CompanyName,
                ContactName = dto.ContactName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Supplier created successfully", supplier.Id });
        }

        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplierDto dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            supplier.CompanyName = dto.CompanyName;
            supplier.ContactName = dto.ContactName;
            supplier.Phone = dto.Phone;
            supplier.Email = dto.Email;
            supplier.Address = dto.Address;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Supplier updated successfully" });
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Supplier deleted successfully" });
        }
    }
}
