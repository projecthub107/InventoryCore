using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;
using InventoryAPI.Services.IServices;
using InventoryAPI.Services;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private readonly InventoryDbContext _context;
        private readonly IProductService _productService;

        public ProductsController(InventoryDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        //// GET: api/Products
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProduct([FromQuery] int? clientId)
        //{
        //    var products = await _productService.GetProductsAsync(clientId);
        //    return Ok(products);

        //    //var list = await _context.Product.ToListAsync();
        //    //return list;
        //}

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? clientId)
        {
            var products = await _productService.GetProductsAsync(clientId);
            return Ok(products);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, [FromQuery] int? clientId)
        {
            var product = await _productService.GetByIdAsync(id, clientId);
            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _productService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _productService.UpdateAsync(id, model);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            

            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
