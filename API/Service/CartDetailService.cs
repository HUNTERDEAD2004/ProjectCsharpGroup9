using API.IService;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Service
{
    public class CartDetailService : ICartDetailService
    {
        AppDbContext _appDbContext;
        public CartDetailService()
        {
            _appDbContext = new AppDbContext();
        }
        public bool CheckOut(Guid UserID)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.UserID == UserID);
            if(user == null) return false;
            else
            {
                var CartItem = _appDbContext.CartsDetails.Where(p => p.CartID == UserID).ToList();
                if (CartItem.Count == 0) return false;
                var bill = new Bill()
                {
                    BillId = Guid.NewGuid(),
                    UserId = UserID,
                    CreateDate = DateTime.Now,
                    Total = 0,
                    Status = 1,
                    BillDetails = new List<BillDetail>()
                };
                _appDbContext.Bills.Add(bill);
                foreach (var item in CartItem)
                {
                    var Product = _appDbContext.Products.Find(item.ProductID);
                    if(Product.Quantity < item.Quantity)
                    {
                        return false;
                    }
                    Product.Quantity -= item.Quantity;
                    var billDetail = new BillDetail()
                    {
                        BillDetailId = Guid.NewGuid(),
                        BillId = bill.BillId,
                        ProductId = item.ProductID,
                        Quantity = item.Quantity,
                        ProductPrice = Product.DiscountPrice,
                        Status = 1
                    };
                    bill.BillDetails.Add(billDetail);
                    _appDbContext.BillDetails.Add(billDetail);
                }
                bill.Total = bill.BillDetails.Sum(p => p.ProductPrice * p.Quantity);
                _appDbContext.CartsDetails.RemoveRange(CartItem);
                _appDbContext.Bills.Add(bill);
                _appDbContext.SaveChanges();
                return true;
            }
        }

        public List<CartDetails> GetCartUser(Guid UserID)
        {
            return _appDbContext.CartsDetails
                .Include(p=>p.Product)
                .Where(p=>p.CartID == UserID)
                .ToList();
        }

        public bool RemoveFromCart(Guid id)
        {
            var CartItem = _appDbContext.CartsDetails.Find(id);
            if(CartItem != null)
            {
                _appDbContext.CartsDetails.Remove(CartItem);
                _appDbContext.SaveChanges();
            }
            return true;
        }
    }
}
