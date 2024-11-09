using System.ComponentModel.DataAnnotations;

namespace FinanceWebApi.DTO
{
    public class BudgetDTO
    {
        [Key]
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
