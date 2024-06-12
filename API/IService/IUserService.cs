using ProjectCsharpGroup9.Models;

namespace API.IService
{
    public interface IUserService
    {
        bool Create(User user);
        int CalculateAge(DateTime birthDate);

        bool Update(User user);
    }
}
