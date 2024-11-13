using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using FinanceWebApi.DTO;
using Microsoft.AspNetCore.Identity;

namespace Finance_Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<UsersController> _logger;
        /// <summary>
        /// Constructor to initilize the DBContext and Logger
        /// </summary>
        /// <param name="context">DBcontext class</param>
        /// <param name="logger">Ilogger</param>
        public UsersController(FinanceDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get all records of Users
        /// </summary>
        /// <returns>Records</returns>
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("Get all the records");
            return await _context.Users.ToListAsync();
        }
        /// <summary>
        /// Get user based on the Id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>User Objects</returns>
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _logger.LogInformation("Get records based on the Id");
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        /// <summary>
        /// Update the records based on the Id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="userDto">UserDto</param>
        /// <returns>updated record</returns>

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDto)
        {
            User user = new User();
            user.UserId=userDto.UserId;
            user.RoleId=userDto.RoleId;
            user.Email=userDto.Email;
            user.FirstName=userDto.FirstName;
            user.LastName=userDto.LastName;
            user.PasswordHash=userDto.PasswordHash;
            if (id != user.UserId)
            {
                return BadRequest();
            }
            _logger.LogInformation("Update details based on id");
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
        /// Create new record
        /// </summary>
        /// <param name="userDto">UserDto object</param>
        /// <returns>New record added to existing records</returns>
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDto)
        {
            User user = new User();
            user.UserId = userDto.UserId;
            user.RoleId = userDto.RoleId;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.PasswordHash = userDto.PasswordHash;
            _context.Users.Add(user);
            _logger.LogInformation("Created new record ");
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }
        /// <summary>
        /// Delete the record based on the Id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>Remaining records </returns>

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _logger.LogInformation("Deleted record based on Id");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
