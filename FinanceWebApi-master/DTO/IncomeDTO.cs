using System.ComponentModel.DataAnnotations;

namespace Finance_Api.DTO
{
    public class IncomeDTO
    {
        [Key]
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public string Source { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
    }
}
