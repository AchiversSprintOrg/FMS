using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;

namespace Finance_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// This is a constructor to initlizr the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>
        public UsersController(FinanceDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method returns the list of Users
        /// </summary>
        /// <returns>Users </returns>

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("Initiated a Get by Action");
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// This method returns the User based on the ID
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User based given id</returns>

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("Initiated a Get by Id Action");
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Update the User based on id, Users
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="user">User Object</param>
        /// <returns>Updated list of Users</returns>

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            _logger.LogInformation("Initiated a Put by Id Action");
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        /// <summary>
        /// Create new Record to the User
        /// </summary>
        /// <param name="userDTO">UserDTo</param>
        /// <returns>Updated User Tabel</returns>

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _logger.LogInformation("Initiated a Post by Id Action");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        /// <summary>
        /// This method Deleted the perticular record based on Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Remaming list of records</returns>

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Initiated a Delete by Id Action");
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
