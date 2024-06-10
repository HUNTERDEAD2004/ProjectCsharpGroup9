namespace ProjectCsharpGroup9.Models
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime InputDay { get; set; }
        public Guid CategoryId { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public Guid BrandID { get; set; }
        public int Quantity { get; set; }
        public double ProductWeight { get; set; }
        public string Description { get; set; }
        public virtual List<Gallery> Galleries { get; set; }
        public virtual List<CartDetails> CartDetails { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual List<FeedBack> FeedBacks { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
