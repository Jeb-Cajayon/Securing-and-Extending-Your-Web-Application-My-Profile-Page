using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models.DB
{
    public class Users
    {
        [Key]
        public int ProfileID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string AccountImage {get; set;}
    }
}
