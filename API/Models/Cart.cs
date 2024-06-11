namespace ProjectCsharpGroup9.Models
{
    public class Cart
    {
        public Guid CartID { get; set; }
        public Guid UserID { get; set; }
        public DateTime CreateDay { get; set; }
        public virtual User? User { get; set; }
        public virtual List<CartDetails>? CartDetails { get; set; }
    }
}
