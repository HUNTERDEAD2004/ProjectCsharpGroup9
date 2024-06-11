using ProjectCsharpGroup9.Models;

namespace API.IService
{
    public interface IProductService
    {
        bool Create(Product product);
        bool AddToCart(Guid id, int quantity, Guid UserID);
    }
}
