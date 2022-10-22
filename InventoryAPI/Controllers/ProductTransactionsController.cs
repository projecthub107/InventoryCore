using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTransactionsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductTransactionsController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTransaction>>> GetProductTransaction()
        {
            return await _context.ProductTransaction.ToListAsync();
        }

        // GET: api/ProductTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTransaction>> GetProductTransaction(int id)
        {
            var productTransaction = await _context.ProductTransaction.FindAsync(id);

            if (productTransaction == null)
            {
                return NotFound();
            }

            return productTransaction;
        }

        // PUT: api/ProductTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductTransaction(int id, ProductTransaction productTransaction)
        {
            if (id != productTransaction.ProductTransactionId)
            {
                return BadRequest();
            }

            _context.Entry(productTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTransactionExists(id))
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

        // POST: api/ProductTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductTransaction>> PostProductTransaction(ProductTransaction productTransaction)
        {
            _context.ProductTransaction.Add(productTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductTransaction", new { id = productTransaction.ProductTransactionId }, productTransaction);
        }

        // DELETE: api/ProductTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductTransaction(int id)
        {
            var productTransaction = await _context.ProductTransaction.FindAsync(id);
            if (productTransaction == null)
            {
                return NotFound();
            }

            _context.ProductTransaction.Remove(productTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductTransactionExists(int id)
        {
            return _context.ProductTransaction.Any(e => e.ProductTransactionId == id);
        }
    }
}
