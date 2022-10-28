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
    public class RoleRightsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public RoleRightsController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/RoleRights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleRight>>> GetRoleRight()
        {
            return await _context.RoleRight.ToListAsync();
        }

        // GET: api/RoleRights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleRight>> GetRoleRight(int id)
        {
            var roleRight = await _context.RoleRight.FindAsync(id);

            if (roleRight == null)
            {
                return NotFound();
            }

            return roleRight;
        }

        // PUT: api/RoleRights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleRight(int id, RoleRight roleRight)
        {
            if (id != roleRight.RoleId)
            {
                return BadRequest();
            }

            _context.Entry(roleRight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleRightExists(id))
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

        // POST: api/RoleRights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoleRight>> PostRoleRight(RoleRight roleRight)
        {
            _context.RoleRight.Add(roleRight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleRight", new { id = roleRight.RoleId }, roleRight);
        }

        // DELETE: api/RoleRights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleRight(int id)
        {
            var roleRight = await _context.RoleRight.FindAsync(id);
            if (roleRight == null)
            {
                return NotFound();
            }

            _context.RoleRight.Remove(roleRight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleRightExists(int id)
        {
            return _context.RoleRight.Any(e => e.RoleId == id);
        }
    }
}
