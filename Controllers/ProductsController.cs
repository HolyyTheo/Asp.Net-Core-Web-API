using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:5269/api/produtcs
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;
        public ProductsController(ProductsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.Where(p => p.IsActive == true).Select(p => ProductToDTO(p)).ToListAsync();

            return Ok(products);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var p = await _context
            .Products
            .Where(i => i.ProductID == id)
            .Select(p => ProductToDTO(p))
            .FirstOrDefaultAsync();

            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductID }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        {
            if (id != entity.ProductID)
            {
                return BadRequest();
            }
            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var delete = await _context.Products.FirstOrDefaultAsync(i => i.ProductID == id);
            if (delete == null)
            {
                return NotFound();
            }
            _context.Products.Remove(delete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }
            return NoContent();
        }
        private static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if (p != null)
            {
                entity.ProductID = p.ProductID;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }
    }
}