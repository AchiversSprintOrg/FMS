using Finance_Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Finance_Api.Models
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual User User { get; set; }

    }
}
