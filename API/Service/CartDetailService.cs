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
        public bool CheckOut(Guid userId)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null)
            {
                return false;
            }
            var cartItems = _appDbContext.CartsDetails.Where(c => c.CartID == userId).ToList();
            if (cartItems.Count == 0)
            {
                return false; 
            }

            var bill = new Bill()
            {
                BillId = Guid.NewGuid(),
                UserId = userId,
                CreateDate = DateTime.Now,
                Total = 0, 
                Status = 1,
                BillDetails = new List<BillDetail>()
            };

            foreach (var cartItem in cartItems)
            {
                var product = _appDbContext.Products.Find(cartItem.ProductID);
                if (product == null || product.Quantity < cartItem.Quantity)
                {
                    return false;
                }
                product.Quantity -= cartItem.Quantity;

                var billDetail = new BillDetail()
                {
                    BillDetailId = Guid.NewGuid(),
                    BillId = bill.BillId,
                    ProductId = cartItem.ProductID,
                    Quantity = cartItem.Quantity,
                    ProductPrice = product.DiscountPrice,
                    Status = 1 // Đang xử lý
                };

                bill.BillDetails.Add(billDetail);
                _appDbContext.BillDetails.Add(billDetail);
            }
            bill.Total = bill.BillDetails.Sum(bd => bd.ProductPrice * bd.Quantity);
            _appDbContext.CartsDetails.RemoveRange(cartItems);
            _appDbContext.Bills.Add(bill);
            _appDbContext.SaveChanges();

            return true;
        }


        public List<CartDetails> GetCartUser(Guid UserID)
        {
            var cartItem = _appDbContext.CartsDetails
                .Include(p => p.Product)
                .Where(p => p.CartID == UserID)
                .Select(p => new CartDetails
                {
                    CartDetailID = p.CartDetailID,
                    ProductID = p.ProductID,
                    CartID = p.CartID,
                    Quantity = p.Quantity,
                    Status = p.Status
                }).ToList();
            return cartItem;
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
