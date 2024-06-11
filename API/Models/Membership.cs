namespace ProjectCsharpGroup9.Models
{
    public class Membership
    {
        public Guid MemberID { get; set; }
        public Guid UserID { get; set; }
        public double Point { get; set; }
        public string Status { get; set; }
        public string MemberShipRank { get; set; }
        public virtual User? User { get; set; }
    }
}
