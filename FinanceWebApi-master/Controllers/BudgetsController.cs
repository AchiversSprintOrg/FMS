using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using Finance_Api.DTO;
using FinanceWebApi.DTO;


namespace Finance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<ExpensesController> _logger;
        /// <summary>
        /// This is a constructor to initialize the readonly property
        /// </summary>
        /// <param name="context">D</param>
        /// <param name="logger"></param>
        public BudgetsController(FinanceDbContext context, ILogger<ExpensesController> logger)
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
            _logger.LogInformation("Get all the records of Budget");
            return await _context.Budgets.ToListAsync();
        }
        /// <summary>
        /// This method returns the Budgets based on the ID
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Budget based given id</returns>
        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudget(int id)
        {
            _logger.LogInformation($"Get the budget records by Id {id}");
            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }
        /// <summary>
        /// Update the Budget based on id, Expenses
        /// </summary>
        /// <param name="id">BudgetId</param>
        /// <param name="budgetDTO">BudgetDTO Object</param>
        /// <returns>Updated list of Budgets</returns>
        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id,BudgetDTO budgetDTO)
        {
            _logger.LogInformation("Put Action initiated");
            Budget budget = new Budget()
            {
                BudgetId = budgetDTO.BudgetId,
                UserId = budgetDTO.UserId,
                Category = budgetDTO.Category,
                Amount = budgetDTO.Amount,
                CreateDate = budgetDTO.CreateDate
            };

            if (id != budget.BudgetId)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                _logger.LogInformation($"update record based on id {budget.BudgetId}");
                await _context.SaveChangesAsync();
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
        /// <returns>Updated Budget Table</returns>
        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Budget>> PostBudget(BudgetDTO budgetDto)
        {
            Budget budget=new Budget();
            budget.BudgetId = budgetDto.BudgetId;
            budget.Amount = budgetDto.Amount;
            budget.UserId=budgetDto.UserId;
            budget.Category = budgetDto.Category;
            budget.CreateDate = budgetDto.CreateDate;
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Created new records");
            return CreatedAtAction("GetBudget", new { id = budget.BudgetId }, budget);
        }
        /// <summary>
        /// This method Deleted the particular record based on Id
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Remaning list of records</returns>
        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            _logger.LogInformation($"Deleted record {id}");
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
