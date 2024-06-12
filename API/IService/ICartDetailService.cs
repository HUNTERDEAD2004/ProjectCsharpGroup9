using ProjectCsharpGroup9.Models;

namespace API.IService
{
    public interface ICartDetailService
    {
        List<CartDetails> GetCartUser(Guid UserID);
        bool RemoveFromCart(Guid id);
        bool CheckOut(Guid UserID);
    }
}
