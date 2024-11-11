using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Finance_Api.Models;
using Finance_Api.DTO;
using Elfie.Serialization;

namespace Finance_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<ExpensesController> _logger;

        public IncomesController(FinanceDbContext context, ILogger<ExpensesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Incomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            _logger.LogInformation("Initiated a Get Action");
            return await _context.Incomes.ToListAsync();
        }

        // GET: api/Incomes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            _logger.LogInformation("Initiated a Get by Id Action");
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Incomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, IncomeDTO incomeDTO)
        {
            _logger.LogInformation("Put Action initiated");

            Income income = new Income()
            {
              IncomeId = incomeDTO.IncomeId,
              Amount = incomeDTO.Amount,
              UserId = incomeDTO.UserId,
              Source = incomeDTO.Source,
              IncomeDate = incomeDTO.IncomeDate,
            };  

            if (id != income.IncomeId)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated Income {income.IncomeId}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
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

        // POST: api/Incomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(IncomeDTO incomeDTO)
        {
            _logger.LogInformation("New Record Added into Database");

            Income income = new Income()
            {
                IncomeId = incomeDTO.IncomeId,
                Amount = incomeDTO.Amount,
                UserId = incomeDTO.UserId,
                Source = incomeDTO.Source,
                IncomeDate = incomeDTO.IncomeDate,
            };

            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.IncomeId }, income);
        }

        // DELETE: api/Incomes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            _logger.LogInformation("Data Deleted Successfully from Database");
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.IncomeId == id);
        }
    }
}
