using System.ComponentModel.DataAnnotations;

namespace Finance_Api.DTO
{
    public class ExpenseDTO
    {
        [Key]
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
