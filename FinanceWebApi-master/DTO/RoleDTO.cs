using System.ComponentModel.DataAnnotations;

namespace Finance_Api.DTO
{
    public class RoleDTO
    {
        [Key]
        public int? RoleId { get; set; }
    }
}
