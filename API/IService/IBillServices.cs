using ProjectCsharpGroup9.Models;

namespace API.IService
{
    public interface IBillServices
    {
        bool CancelPayment (Guid Id);
        bool BuyAgain(Guid Id);
    }
}
