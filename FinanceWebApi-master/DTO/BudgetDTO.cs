using System.ComponentModel.DataAnnotations;

namespace Finance_Api.DTO
{
    public class BudgetDTO
    {
        [Key]
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
