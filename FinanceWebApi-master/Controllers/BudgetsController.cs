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
    public class BudgetsController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<ExpensesController> _logger;

        /// <summary>
        /// This is a constructor to initlize the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>
        /// <param name="logger">ILogger object</param>

        public BudgetsController(FinanceDbContext context, ILogger<ExpensesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method returns the list of Budgets
        /// </summary>
        /// <returns> Budgets </returns>

        // GET: api/Budgets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
            _logger.LogInformation("Initiated a Get Action");
            return await _context.Budgets.ToListAsync();
        }

        /// <summary>
        /// This method returns the Budget based on the ID
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Budget based on given id</returns>

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetBudget(int id)
        {
            _logger.LogInformation("Initiated a Get by Id Action");
            var budget = await _context.Budgets.FindAsync(id);
            _logger.LogInformation($"Get details based on Id {id}");

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
        /// <param name="budgetDTO">BudgetDTO</param>
        /// <returns>Updated list of Budgets</returns>

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, BudgetDTO budgetDto)
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
        /// <param name="budgetDTO">BudgetDTO</param>
        /// <returns>Updated Budget Tabel</returns>

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
        /// This method will delete the particular record from Database based on Id
        /// </summary>
        /// <param name="id">Budget Id</param>
        /// <returns>Remaining List of the Budgets will be displayed</returns>

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
