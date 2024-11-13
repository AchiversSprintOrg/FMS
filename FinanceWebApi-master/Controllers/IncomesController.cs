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
    /// <summary>
    /// Controller to initilize DBContext and Logger
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly FinanceDbContext _context;
        private readonly ILogger<IncomesController> _logger;

       public IncomesController(FinanceDbContext context, ILogger<IncomesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Get the details of Income
        /// </summary>
        /// <returns>all the records</returns>
        // GET: api/Incomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            _logger.LogInformation("Get records of Income");
            return await _context.Incomes.ToListAsync();
        }

        /// <summary>
        /// Get details based on Id
        /// </summary>
        /// <param name="id">IncomeId</param>
        /// <returns>Record on Id</returns>
        // GET: api/Incomes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            _logger.LogInformation($"Get details based on Id {id}");
            if (income == null)
            {
                return NotFound();
            }

            return income;
        }
        /// <summary>
        /// Update the record based on Id
        /// </summary>
        /// <param name="id">IncomeId</param>
        /// <param name="incomeDto">IncomeDTO class</param>
        /// <returns>updated record on id</returns>

        // PUT: api/Incomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, IncomeDTO incomeDto)
        {
            Income income = new Income();
            income.IncomeId = incomeDto.IncomeId;
            income.UserId = incomeDto.UserId;
            income.Source = incomeDto.Source;
            income.Amount = incomeDto.Amount;
            income.IncomeDate = incomeDto.IncomeDate;
            if (id != income.IncomeId)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;
            _logger.LogInformation($"Update records based on the Id {income.IncomeId}");
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
        /// Create new records based on the class details
        /// </summary>
        /// <param name="incomeDto">IncomeDto class</param>
        /// <returns>Creates new record to existing class</returns>

        // POST: api/Incomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(IncomeDTO incomeDto)
        {
            Income income = new Income();
            income.IncomeId = incomeDto.IncomeId;
            income.UserId = incomeDto.UserId;
            income.Source = incomeDto.Source;
            income.Amount = incomeDto.Amount;
            income.IncomeDate = incomeDto.IncomeDate;
            _context.Incomes.Add(income);
            _logger.LogInformation("Created new Records");
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.IncomeId }, income);
        }

        /// <summary>
        /// Deletes the records based on Id
        /// </summary>
        /// <param name="id">Income Id</param>
        /// <returns>Remaining records</returns>
        // DELETE: api/Incomes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            _logger.LogInformation("Removed the record");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return _context.Incomes.Any(e => e.IncomeId == id);
        }
    }
}
