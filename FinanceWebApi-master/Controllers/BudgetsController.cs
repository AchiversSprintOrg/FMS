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
    public class BudgetsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<BudgetsController> _logger;

        /// <summary>
        /// This is a constructor to initialize the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>
        
        public BudgetsController(FinanceDbContext context, ILogger<BudgetsController> logger)
        {
            _context = context;
            _logger = logger;
        }
                /// <summary>
        /// This method returns the list of Budget
        /// </summary>
        /// <returns>Budgets </returns>

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
            _logger.LogInformation("Initiated a Get Action");
            return await _context.Budgets.ToListAsync();
        }

        /// <summary>
        /// This method returns the Budgets based on the ID
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Budget based given id</returns>

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudget(int id)
        {
            _logger.LogInformation("Initiated a Get by Id Action");
            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        /// <summary>
        /// Update the Budgets based on id, Budgets
        /// </summary>
        /// <param name="id">BudgetId</param>
        /// <param name="budget">Budget Object</param>
        /// <returns>Updated list of Budgets</returns>

        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, BudgetDTO budgetDTO)
        {
            _logger.LogInformation("Put Action initiated");

            Budget budget = new Budget()
            {
                BudgetId = budgetDTO.BudgetId,
                UserId = budgetDTO.UserId,
                Category = budgetDTO.Category,
                Amount = budgetDTO.Amount,
                CreatedDate = budgetDTO.CreatedDate,
            };

            if (id != budget.BudgetId)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated Budget {budget.BudgetId}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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
        /// Create new Record to the Budget
        /// </summary>
        /// <param name="budgetDTO">BudgetDTo</param>
        /// <returns>Updated Budget Tabel</returns>

        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Budget>> PostBudget(BudgetDTO budgetDTO)
        {
            _logger.LogInformation("New Record Added into Database");
            Budget budget = new Budget()
            {
                BudgetId = budgetDTO.BudgetId,
                UserId = budgetDTO.UserId,
                Category = budgetDTO.Category,
                Amount = budgetDTO.Amount,
                CreatedDate = budgetDTO.CreatedDate,
            };
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudget", new { id = budget.BudgetId }, budget);
        }

        /// <summary>
        /// This method Deleted the particular record based on Id
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Remaining list of records</returns>

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            _logger.LogInformation("Data Deleted Successfully from Database");
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.BudgetId == id);
        }
    }
}
