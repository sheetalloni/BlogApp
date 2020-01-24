using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogApp;

namespace BlogApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsersController : ControllerBase
    {
        private readonly BlogContext _context;

        public LoginUsersController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/LoginUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginUser>>> GetLoginUsers()
        {
            return await _context.LoginUsers.ToListAsync();
        }

        // GET: api/LoginUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginUser>> GetLoginUser(int id)
        {
            var loginUser = await _context.LoginUsers.FindAsync(id);

            if (loginUser == null)
            {
                return NotFound();
            }

            return loginUser;
        }

        // PUT: api/LoginUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginUser(int id, LoginUser loginUser)
        {
            if (id != loginUser.id)
            {
                return BadRequest();
            }

            _context.Entry(loginUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginUserExists(id))
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

        // POST: api/LoginUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LoginUser>> PostLoginUser(LoginUser loginUser)
        {
            _context.LoginUsers.Add(loginUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginUser", new { id = loginUser.id }, loginUser);
        }

        // DELETE: api/LoginUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoginUser>> DeleteLoginUser(int id)
        {
            var loginUser = await _context.LoginUsers.FindAsync(id);
            if (loginUser == null)
            {
                return NotFound();
            }

            _context.LoginUsers.Remove(loginUser);
            await _context.SaveChangesAsync();

            return loginUser;
        }

        private bool LoginUserExists(int id)
        {
            return _context.LoginUsers.Any(e => e.id == id);
        }
    }
}
