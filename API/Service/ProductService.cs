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
