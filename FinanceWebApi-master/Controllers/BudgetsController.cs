using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using FinanceWebApi.DTO;

namespace Finance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<ExpensesController> _logger;
        public BudgetsController(FinanceDbContext context, ILogger<ExpensesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
            _logger.LogInformation("Initiated a Get Action");
            return await _context.Budgets.ToListAsync();
        }

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
