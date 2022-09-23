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
    public class SP_UserInfoController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public SP_UserInfoController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/SP_UserInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SP_UserInfo>>> GetSP_UserInfo()
        {
            string StoredProc = "exec [SP_UserInfo]";

          //  string sql = "SELECT * FROM [UserInfo]";

            return await _context.SP_UserInfo.FromSqlRaw(StoredProc).ToListAsync();
        }

        //// GET: api/SP_UserInfo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<SP_UserInfo>> GetSP_UserInfo(int id)
        //{
        //    var sP_UserInfo = await _context.SP_UserInfo.FindAsync(id);

        //    if (sP_UserInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return sP_UserInfo;
        //}

        //// PUT: api/SP_UserInfo/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSP_UserInfo(int id, SP_UserInfo sP_UserInfo)
        //{
        //    if (id != sP_UserInfo.UserId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(sP_UserInfo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SP_UserInfoExists(id))
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

        //// POST: api/SP_UserInfo
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<SP_UserInfo>> PostSP_UserInfo(SP_UserInfo sP_UserInfo)
        //{
        //    _context.SP_UserInfo.Add(sP_UserInfo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSP_UserInfo", new { id = sP_UserInfo.UserId }, sP_UserInfo);
        //}

        //// DELETE: api/SP_UserInfo/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSP_UserInfo(int id)
        //{
        //    var sP_UserInfo = await _context.SP_UserInfo.FindAsync(id);
        //    if (sP_UserInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.SP_UserInfo.Remove(sP_UserInfo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SP_UserInfoExists(int id)
        //{
        //    return _context.SP_UserInfo.Any(e => e.UserId == id);
        //}
    }
}
