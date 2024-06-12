using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectCsharpGroup9.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int RoleID { get; set; }
        [Required]
        [StringLength(450, MinimumLength = 10, ErrorMessage ="độ dài từ 10 đến 450")]
        public string UserName { get; set; }
        [Required]
        [StringLength(450, MinimumLength = 10, ErrorMessage = "độ dài từ 10 đến 450")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [RegularExpression("^(?:\\+84|0)(?:3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-4|6-9])[0-9]{7}$",
            ErrorMessage ="Số điện thoại sai định dạng")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public virtual List<Bill>? Bills { get; set; }
        public virtual List<FeedBack>? FeedBacks { get; set; }
        public virtual Membership? Membership { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Role? Role { get; set; }
    }
}
