using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Data;
using InventoryAPI.Models.SP;

namespace InventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SP_ProductController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public SP_ProductController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/SP_Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SP_Product>>> GetSP_Product()
        {
            string StoredProc = "exec [SP_Product] @KeyWord = ''";

            //  string sql = "SELECT * FROM [UserInfo]";

            return await _context.SP_Product.FromSqlRaw(StoredProc).ToListAsync();
            //return await _context.SP_Product.ToListAsync();
        }

        // GET: api/SP_Product
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<SP_Product>>> GetSP_Products(string search)
        {
            string StoredProc = "exec [SP_Product] " +
                                "@KeyWord = '" + search + "'";

            return await _context.SP_Product.FromSqlRaw(StoredProc).ToListAsync();

        }

        //// GET: api/SP_Product/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SP_Product>> GetSP_Product(int id)
        //{
        //    var sP_Product = await _context.SP_Product.FindAsync(id);

        //    if (sP_Product == null)
        //    {
        //        return NotFound();
        //    }

        //    return sP_Product;
        //}

        //// PUT: api/SP_Product/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSP_Product(int id, SP_Product sP_Product)
        //{
        //    if (id != sP_Product.ProductId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sP_Product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SP_ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/SP_Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SP_Product>>> PostSP_Product(SP_Product product)
        {

            string StoredProc = "exec [SP_Product] @KeyWord = ''";

            //  string sql = "SELECT * FROM [UserInfo]";

            return await _context.SP_Product.FromSqlRaw(StoredProc).ToListAsync();

            //_context.SP_Product.Add(product);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetSP_Product", new { id = product.ProductId }, product);


            //string StoredProc = "exec CreateProduct " +
            //"@ProductCode = '" + product.ProductCode + "'," +
            //"@ProductName= '" + product.ProductName + "'," +
            //"@ManufacturerName= '" + product.ManufacturerName + "'," +
            //"@AreaName= " + product.AreaName + "," +
            //"@ClientId= '" + product.ClientId + "'," +
            //"@CreatedDate= '" + DateTime.Now + "'," +
            //"@CreatedBy= '" + product.CreatedBy + "'," +
            //"@CreatedDate= '" + DateTime.Now + "'," +
            //"@CreatedBy= '" + product.ModifiedBy + "'";

            //return await _context.SP_Product.FromSqlRaw(StoredProc).ToListAsync();

            //---------------------------------------------------------------------------------
            //product = await _context.SP_Product.FromSqlRaw(StoredProc).FirstAsync();

            //return CreatedAtAction("GetSP_Product", new { id = product.ProductId }, product);

            //return await _context.output.ToListAsync();

        }

        //// DELETE: api/SP_Product/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSP_Product(int id)
        //{
        //    var sP_Product = await _context.SP_Product.FindAsync(id);
        //    if (sP_Product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SP_Product.Remove(sP_Product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SP_ProductExists(int id)
        //{
        //    return _context.SP_Product.Any(e => e.ProductId == id);
        //}
    }
}
