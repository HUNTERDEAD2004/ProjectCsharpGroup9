using API.IService;
using Microsoft.EntityFrameworkCore;
using ProjectCsharpGroup9.Models;

namespace API.Service
{
    public class BillServices : IBillServices
    {
        AppDbContext _dbContext;

        public BillServices()
        {
            _dbContext = new AppDbContext();
        }

        public bool BuyAgain(Guid Id)
        {
            try
            {
                var bill = _dbContext.Bills.Include(b => b.BillDetails).FirstOrDefault(b => b.BillId == Id);
                foreach (var billDetail in bill.BillDetails)
                {
                    var product = _dbContext.Products.Find(billDetail.ProductId);
                    if (product != null)
                    {
                        var cartItem = _dbContext.CartsDetails.FirstOrDefault(p => p.ProductID == billDetail.ProductId);
                        if (cartItem == null)
                        {
                            cartItem = new CartDetails
                            {
                                CartDetailID = Guid.NewGuid(),
                                CartID = bill.UserId,
                                ProductID = billDetail.ProductId,
                                Quantity = billDetail.Quantity,
                                Status = "Còn Hàng",
                            };
                            _dbContext.CartsDetails.Add(cartItem);
                        }
                        else
                        {
                            cartItem.Quantity += billDetail.Quantity;
                            _dbContext.CartsDetails.Update(cartItem);
                        }
                    }
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CancelPayment(Guid Id)
        {
            try
            {
                var bill = _dbContext.Bills.FirstOrDefault(b => b.BillId == Id);
                var billDetail = _dbContext.BillDetails.Where(d => d.BillId == Id).ToList();
                foreach (var item in billDetail)
                {
                    var product = _dbContext.Products.Find(item.ProductId);
                    if (product != null)
                    {
                        product.Quantity += item.Quantity;
                    }
                }
                bill.Status = 100;
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
