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
    public class IncomesController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<IncomesController> _logger;

        /// <summary>
        /// This is a constructor to initlizr the readonly property 
        /// </summary>
        /// <param name="context">DBcontext object</param>

        public IncomesController(FinanceDbContext context, ILogger<IncomesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This method returns the list of Incomes
        /// </summary>
        /// <returns>Incomes </returns>

        // GET: api/Incomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            _logger.LogInformation("Initiated a Get by Action");
            return await _context.Incomes.ToListAsync();
        }

        /// <summary>
        /// This method returns the Income based on the ID
        /// </summary>
        /// <param name="id">Income Id</param>
        /// <returns>Income based given id</returns>

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

        /// <summary>
        /// Update the Income based on id, Incomes
        /// </summary>
        /// <param name="id">IncomeId</param>
        /// <param name="income">Income Object</param>
        /// <returns>Updated list of Incomes</returns>

        // PUT: api/Incomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, IncomeDTO incomeDTO)
        {
            _logger.LogInformation("Put Action initiated");
            Income income = new Income()
            {
                IncomeId = incomeDTO.IncomeId,
                UserId = incomeDTO.UserId,
                Source = incomeDTO.Source,
                Amount = incomeDTO.Amount,
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

        /// <summary>
        /// Create new Record to the Income
        /// </summary>
        /// <param name="incomeDTO">IncomeDTo</param>
        /// <returns>Updated Income Tabel</returns>

        // POST: api/Incomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(IncomeDTO incomeDTO)
        {
            _logger.LogInformation("Post Action initiated");
            Income income = new Income()
            {
                IncomeId = incomeDTO.IncomeId,
                UserId = incomeDTO.UserId,
                Source = incomeDTO.Source,
                Amount = incomeDTO.Amount,
                IncomeDate = incomeDTO.IncomeDate,

            };
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.IncomeId }, income);
        }

        /// <summary>
        /// This method Deleted the perticular record based on Id
        /// </summary>
        /// <param name="id">Income Id</param>
        /// <returns>Remaming list of records</returns>

        // DELETE: api/Incomes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            _logger.LogInformation("Removed from database");
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
