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
    public class BranchesController : ControllerBase
    {
        private readonly PosDbContext _context;
        public BranchesController(PosDbContext context)
        {
            _context = context;
        }
        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchesDto>>> GetAll()
        {
            var branches = await _context.Branches
                .Select(b => new BranchesDto
                {
                    Id = b.Id,
                    BranchName = b.BranchName,
                    Location = b.Location
                })
                .ToListAsync();

            return Ok(branches);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchesDto>> GetById(int id)
        {
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null) return NotFound();

            var dto = new BranchesDto
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                Location = branch.Location
            };

            return Ok(dto);
        }

        // POST: api/Branches
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BranchesDto dto)
        {
            var branch = new Branches
            {
                BranchName = dto.BranchName,
                Location = dto.Location
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            dto.Id = branch.Id;
            return CreatedAtAction(nameof(GetById), new { id = branch.Id }, dto);
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BranchesDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            branch.BranchName = dto.BranchName;
            branch.Location = dto.Location;

            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null) return NotFound();

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Branch deleted successfully." });
        }
    }
}
