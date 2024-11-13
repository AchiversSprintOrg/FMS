using System.ComponentModel.DataAnnotations;

namespace FinanceWebApi.DTO
{
    public class UserDTO
    {
        [Key]
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int RoleId { get; set; }
       
    }
}
