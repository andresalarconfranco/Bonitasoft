using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_SECURITY.Models;

namespace API_SECURITY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicRolesController : ControllerBase
    {
        private readonly PICA_SECURITYContext _context;

        public PicRolesController(PICA_SECURITYContext context)
        {
            _context = context;
        }

        // GET: api/PicRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PicRoles>>> GetPicRoles()
        {
            return await _context.PicRoles.ToListAsync();
        }

        // GET: api/PicRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PicRoles>> GetPicRoles(int id)
        {
            var picRoles = await _context.PicRoles.FindAsync(id);

            if (picRoles == null)
            {
                return NotFound();
            }

            return picRoles;
        }

        // PUT: api/PicRoles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicRoles(int id, PicRoles picRoles)
        {
            if (id != picRoles.RolNcode)
            {
                return BadRequest();
            }

            _context.Entry(picRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PicRolesExists(id))
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

        // POST: api/PicRoles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PicRoles>> PostPicRoles(PicRoles picRoles)
        {
            _context.PicRoles.Add(picRoles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PicRolesExists(picRoles.RolNcode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPicRoles", new { id = picRoles.RolNcode }, picRoles);
        }

        // DELETE: api/PicRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PicRoles>> DeletePicRoles(int id)
        {
            var picRoles = await _context.PicRoles.FindAsync(id);
            if (picRoles == null)
            {
                return NotFound();
            }

            _context.PicRoles.Remove(picRoles);
            await _context.SaveChangesAsync();

            return picRoles;
        }

        private bool PicRolesExists(int id)
        {
            return _context.PicRoles.Any(e => e.RolNcode == id);
        }
    }
}
