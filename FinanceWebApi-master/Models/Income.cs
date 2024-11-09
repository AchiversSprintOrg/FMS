using System.ComponentModel.DataAnnotations;

namespace Finance_Api.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public virtual User User { get; set; }

    }
}
