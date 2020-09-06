using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_SECURITY.Models;
using Microsoft.Extensions.Logging;

namespace API_SECURITY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicUsersController : ControllerBase
    {


        private readonly ILogger<PicUsersController> _logger;
        private readonly PICA_SECURITYContext _context;

        public PicUsersController(ILogger<PicUsersController> logger, PICA_SECURITYContext context)
        {
            _logger = logger;
            _context = context;

        }

        // GET: api/PicUsers
        [HttpGet]
        public async Task< ActionResult<IEnumerable<PicUsers>>> GetPicUsers()
        {  
            return await _context.PicUsers.ToListAsync();
            
        }

        // GET: api/PicUsers/5
      
        [HttpGet("{id}")]
        
        public async Task<ActionResult<PicUsers>> GetPicUsers(int id)
        {
            var picUsers = await _context.PicUsers.FindAsync(id);

            if (picUsers == null)
            {
                return NotFound();
            }

            return picUsers;
        }

        // PUT: api/PicUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicUsers(int id, PicUsers picUsers)
        {
            if (id != picUsers.UseNcode)
            {
                return BadRequest();
            }

            _context.Entry(picUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PicUsersExists(id))
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

        // POST: api/PicUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PicUsers>> PostPicUsers(PicUsers picUsers)
        {
            _context.PicUsers.Add(picUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicUsers", new { id = picUsers.UseNcode }, picUsers);
        }

        // DELETE: api/PicUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PicUsers>> DeletePicUsers(int id)
        {
            var picUsers = await _context.PicUsers.FindAsync(id);
            if (picUsers == null)
            {
                return NotFound();
            }

            _context.PicUsers.Remove(picUsers);
            await _context.SaveChangesAsync();

            return picUsers;
        }

        private bool PicUsersExists(int id)
        {
            return _context.PicUsers.Any(e => e.UseNcode == id);
        }
    }
}
