namespace ProjectCsharpGroup9.Models
{
    public class CartDetails
    {
        public Guid CartDetailID { get; set; }
        public Guid CartID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
