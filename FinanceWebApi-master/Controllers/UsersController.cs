﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using Finance_Api.DTO;

namespace Finance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<FinanceDbContext> _logger;

        /// <summary>
        /// This is a constructor to initlize the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>

        public UsersController(FinanceDbContext context, ILogger<FinanceDbContext> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method returns the list of Users
        /// </summary>
        /// <returns>User </returns>

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("Get Action Initiated");
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Update the User based on id, Users
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="user">Users Object</param>
        /// <returns>Updated list of Users</returns>

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation("Get  by Id Action Initiated");

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Create new Record to the User
        /// </summary>
        /// <param name="userDTO">UserDTO</param>
        /// <returns>Updated User Tabel</returns>

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            _logger.LogInformation("Put Action initiated");

            User user = new User()
            {
                UserId = userDTO.UserId,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                RoleId = userDTO.RoleId,
            };


            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated UserId {user.UserId} ");
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
        /// This method Deleted the particular record based on Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Remaining list of records</returns>

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDTO)
        {
            _logger.LogInformation("New Record Added into Database");

            User user = new User()
            {
                UserId = userDTO.UserId,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                PasswordHash = userDTO.PasswordHash,
                RoleId = userDTO.RoleId,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Data Deleted Successfully from Database");

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
