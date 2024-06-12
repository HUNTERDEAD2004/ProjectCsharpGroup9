using API.IService;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Service
{
    public class UserService : IUserService
    {
        AppDbContext _dbContext;
        public UserService()
        {
            _dbContext = new AppDbContext();
        }
        public bool Create(User user)
        {
            try
            {
                if (_dbContext.Users.Any(u => u.UserName == user.UserName))
                {
                    return false;
                }

                if (_dbContext.Users.Any(u => u.Email == user.Email))
                {
                    return false;
                }

                if (_dbContext.Users.Any(u => u.PhoneNumber == user.PhoneNumber))
                {
                    return false;
                }
                _dbContext.Users.Add(user);
                //đăng ký thì sẽ tạo luôn giỏ hàng
                var CartUser = new Cart()
                {
                    CartID = user.UserID,
                    UserID = user.UserID,
                    CreateDay = DateTime.Now
                };
                var Mem = new Membership()
                {
                    MemberID = user.UserID,
                    UserID = user.UserID,
                    Point = 0,
                    Status = "0",
                    MemberShipRank = "Iron"
                };
                _dbContext.Memberships.Add(Mem);
                _dbContext.Carts.Add(CartUser);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }

        public bool Update(User user)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.UserID == user.UserID);
            if (existingUser == null)
            {
                return false;
            }

            if (_dbContext.Users.Any(u => u.UserName == user.UserName && u.UserID != user.UserID))
            {
                return false;
            }

            if (_dbContext.Users.Any(u => u.Email == user.Email && u.UserID != user.UserID))
            {
                return false;
            }

            if (_dbContext.Users.Any(u => u.PhoneNumber == user.PhoneNumber && u.UserID != user.UserID))
            {
                return false;
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
