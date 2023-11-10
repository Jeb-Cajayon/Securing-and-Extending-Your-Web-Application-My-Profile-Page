using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models.DB
{
    public class UserRole
    {
        [Key]
        public int RoleID { get; set; }
        public int UserID { get; set; }
        public int LookUpRoleID { get; set; } 
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}

