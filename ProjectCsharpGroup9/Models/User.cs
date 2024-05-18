using Microsoft.AspNetCore.Identity;

namespace ProjectCsharpGroup9.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int RoleID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual List<Bill> Bills { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Role Role { get; set; }
    }
}
