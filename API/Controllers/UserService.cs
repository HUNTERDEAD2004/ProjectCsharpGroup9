using API.IService;
using ProjectCsharpGroup9.Models;

namespace API.Controllers
{
    public class UserService : IUserService
    {
		AppDbContext _dbcobtext;
        public UserService()
        {
            _dbcobtext = new AppDbContext();
        }
        public bool UpdateUser(User user)
        {
			try
			{
                var a = _dbcobtext.Users.Find(user.UserID);
                a.FullName = user.FullName;
                a.BirthDay = user.BirthDay;
                a.RoleID = user.RoleID;
                a.UserName = user.UserName;
                a.Password = user.Password;
                a.Email = user.Email;
                a.PhoneNumber = user.PhoneNumber;
                a.Address = user.Address;
                _dbcobtext.Users.Update(a);
                _dbcobtext.SaveChanges();
                return true;
			}
			catch (Exception)
			{
                return false;
			}
        }
    }
}
