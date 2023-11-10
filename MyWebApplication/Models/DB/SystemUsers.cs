using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models.DB
{
    public class SystemUsers
    {
        [Key]
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string PasswordEncryptedText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
