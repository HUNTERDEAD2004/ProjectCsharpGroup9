using Microsoft.AspNetCore.Mvc;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Controllers
{
    public class MembershipController : ControllerBase
    {
        AppDbContext _dbContext;

        public MembershipController()
        {
            _dbContext = new AppDbContext();
        }

    }
}
