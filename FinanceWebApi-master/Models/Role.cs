using System.ComponentModel.DataAnnotations;

namespace Finance_Api.Models
{
    public class Role
    {
        [Key]
        public int? RoleId { get; set; }
    }
}
