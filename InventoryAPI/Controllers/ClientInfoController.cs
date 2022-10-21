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
    public class ClientInfosController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ClientInfosController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/ClientInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientInfo>>> GetClientInfo()
        {
            return await _context.ClientInfo.ToListAsync();
        }

        // GET: api/ClientInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientInfo>> GetClirntInfo(int id)
        {
            var clientInfo = await _context.ClientInfo.FindAsync(id);

            if (clientInfo == null)
            {
                return NotFound();
            }

            return clientInfo;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientInfo(int id, ClientInfo clientInfo)
        {
            if (id != clientInfo.ClientId)
            {
                return BadRequest();
            }

            _context.Entry(clientInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientInfoExists(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostClientInfo(ClientInfo clientInfo)
        {
            _context.ClientInfo.Add(clientInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientInfo", new { id = clientInfo.ClientId }, clientInfo);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientInfo(int id)
        {
            var clientInfo = await _context.ClientInfo.FindAsync(id);
            if (clientInfo == null)
            {
                return NotFound();
            }

            _context.ClientInfo.Remove(clientInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientInfoExists(int id)
        {
            return _context.ClientInfo.Any(e => e.ClientId == id);
        }
    }
}
