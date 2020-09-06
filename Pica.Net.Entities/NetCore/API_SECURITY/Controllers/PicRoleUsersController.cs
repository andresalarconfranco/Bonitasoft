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
    public class PicRoleUsersController : ControllerBase
    {
        private readonly PICA_SECURITYContext _context;

        public PicRoleUsersController(PICA_SECURITYContext context)
        {
            _context = context;
        }

        // GET: api/PicRoleUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PicRoleUser>>> GetPicRoleUser()
        {
            return await _context.PicRoleUser.ToListAsync();
        }

        // GET: api/PicRoleUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PicRoleUser>> GetPicRoleUser(int id)
        {
            var picRoleUser = await _context.PicRoleUser.FindAsync(id);

            if (picRoleUser == null)
            {
                return NotFound();
            }

            return picRoleUser;
        }

        // PUT: api/PicRoleUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicRoleUser(int id, PicRoleUser picRoleUser)
        {
            if (id != picRoleUser.RusNcode)
            {
                return BadRequest();
            }

            _context.Entry(picRoleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PicRoleUserExists(id))
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

        // POST: api/PicRoleUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PicRoleUser>> PostPicRoleUser(PicRoleUser picRoleUser)
        {
            _context.PicRoleUser.Add(picRoleUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPicRoleUser", new { id = picRoleUser.RusNcode }, picRoleUser);
        }

        // DELETE: api/PicRoleUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PicRoleUser>> DeletePicRoleUser(int id)
        {
            var picRoleUser = await _context.PicRoleUser.FindAsync(id);
            if (picRoleUser == null)
            {
                return NotFound();
            }

            _context.PicRoleUser.Remove(picRoleUser);
            await _context.SaveChangesAsync();

            return picRoleUser;
        }

        private bool PicRoleUserExists(int id)
        {
            return _context.PicRoleUser.Any(e => e.RusNcode == id);
        }
    }
}
