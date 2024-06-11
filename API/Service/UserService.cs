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
    }
}
