using System.ComponentModel.DataAnnotations;

namespace Finance_Api.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }

    }
}
