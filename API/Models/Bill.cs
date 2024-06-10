namespace ProjectCsharpGroup9.Models
{
    public class Bill
    {
        public Guid BillId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Total { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
