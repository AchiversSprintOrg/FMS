using System;
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
    public class ExpensesController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<ExpensesController> _logger;

        /// <summary>
        /// This is a constructor to initlizr the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>
        public ExpensesController(FinanceDbContext context, ILogger<ExpensesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method returns the list of Expenses
        /// </summary>
        /// <returns>Expenses </returns>

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            _logger.LogInformation("Received a Get Request");
            return await _context.Expenses.ToListAsync();
        }

        /// <summary>
        /// This method returns the Expense based on the ID
        /// </summary>
        /// <param name="id">Expense Id</param>
        /// <returns>Expense based given id</returns>
        
        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            _logger.LogInformation("Received a Get Request");

            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        /// <summary>
        /// Update the Expenses based on id, Expenses
        /// </summary>
        /// <param name="id">ExpenseId</param>
        /// <param name="expense">Expense Object</param>
        /// <returns>Updated list of Expenses</returns>
        
        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, ExpenseDTO expenseDTO)
        {
            _logger.LogInformation("Received Put Request");

            Expense expense = new Expense()
            {
                UserId = expenseDTO.UserId,
                ExpenseId = expenseDTO.ExpenseId,
                Category = expenseDTO.Category,
                ExpenseDate = expenseDTO.ExpenseDate,
                Amount = expenseDTO.Amount,
            };

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated Expense {expense.ExpenseId}");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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
        /// Create new Record to the Expense
        /// </summary>
        /// <param name="expenseDTO">ExpenseDTo</param>
        /// <returns>Updated Expense Tabel</returns>
        
        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDTO expenseDTO)
        {
            Expense expense = new Expense()
            {
                UserId = expenseDTO.UserId,
                ExpenseId = expenseDTO.ExpenseId,
                Category = expenseDTO.Category,
                ExpenseDate = expenseDTO.ExpenseDate,
                Amount = expenseDTO.Amount,
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        /// <summary>
        /// This method Deleted the perticular record based on Id
        /// </summary>
        /// <param name="id">Expense Id</param>
        /// <returns>Remaming list of records</returns>
        
        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
