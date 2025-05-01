using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
    public class ProductController : ControllerBase
    {
        private readonly PosDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(PosDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        {
            var products = await _context.Products.ToListAsync();

            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                ManufactureDate = p.ManufactureDate,
                ExpiryDate = p.ExpiryDate,
                BranchId = p.BranchId,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId,
                PhotoUrl = !string.IsNullOrEmpty(p.Photo) ? $"{baseUrl}/images/{p.Photo}" : null
            });

            return Ok(productDtos);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                StockQuantity = product.StockQuantity,
                Price = product.Price,
                ExpiryDate = product.ExpiryDate,
                ManufactureDate = product.ManufactureDate,
                BranchId = product.BranchId,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
                PhotoUrl = string.IsNullOrEmpty(product.Photo)
                    ? null
                    : $"{Request.Scheme}://{Request.Host}/images/{product.Photo}"
            };

            return Ok(productDto);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto dto)
        {
            string? fileName = null;

            if (dto.Photo != null)
            {
                string uploadDir = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Photo.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }
            }

            var product = new Product
            {
                Name = dto.Name,
                Photo = fileName,
                StockQuantity = dto.StockQuantity,
                Price = dto.Price,
                ExpiryDate = dto.ExpiryDate,
                ManufactureDate = dto.ManufactureDate,
                BranchId = dto.BranchId,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.StockQuantity = dto.StockQuantity;
            product.Price = dto.Price;
            product.ExpiryDate = dto.ExpiryDate;
            product.ManufactureDate = dto.ManufactureDate;
            product.BranchId = dto.BranchId;
            product.CategoryId = dto.CategoryId;
            product.SupplierId = dto.SupplierId;

            // Handle image replacement
            if (dto.Photo != null)
            {
                // Delete old photo if exists
                if (!string.IsNullOrEmpty(product.Photo))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "images", product.Photo);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // Save new photo
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Photo.FileName)}";
                var savePath = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }

                product.Photo = fileName;
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product updated successfully." });
        }


        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            // Delete image from wwwroot/Uploads
            if (!string.IsNullOrEmpty(product.Photo))
            {
                var imagePath = Path.Combine(_env.WebRootPath, "images", product.Photo);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product and associated image deleted successfully." });
        }

    }
}
