using API.IService;
using ProjectCsharpGroup9.Models;

namespace API.Service
{
    public class ProductService : IProductService
    {
        AppDbContext _dbcontext;
        public ProductService()
        {
            _dbcontext = new AppDbContext();
        }

        public bool AddToCart(Guid id, int quantity, Guid UserID)
        {
            try
            {
                var product = _dbcontext.Products.FirstOrDefault(p => p.ProductID == id);
                if (quantity > product.Quantity) return false;
                var CartItem = _dbcontext.CartsDetails.FirstOrDefault(p => p.ProductID == id && p.CartID == UserID);
                if (CartItem == null)
                {
                    CartDetails cartDetails = new CartDetails()
                    {
                        CartDetailID = Guid.NewGuid(),
                        ProductID = id,
                        CartID = UserID,
                        Quantity = quantity,
                        Status = "chua thanh toan"
                    };
                    _dbcontext.CartsDetails.Add(cartDetails);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    CartItem.Quantity = CartItem.Quantity + quantity;
                    _dbcontext.CartsDetails.Update(CartItem);
                    _dbcontext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }  
        }

        public bool Create(Product product)
        {
            try
            {
                _dbcontext.Products.Add(product);
                Gallery gallery = new Gallery()
                {
                    GalleryID = Guid.NewGuid(),
                    ProductID = product.ProductID,
                    ImagePath = "",
                    Description = "",
                };
                _dbcontext.Gallerys.Add(gallery);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
