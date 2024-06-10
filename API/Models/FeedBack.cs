namespace ProjectCsharpGroup9.Models
{
    public class FeedBack
    {
        public Guid FeedBackID { get; set; }
        public string Content { get; set; }
        public Guid ProductID { get; set; }
        public DateTime DateTime { get; set; }
        public Guid UserID { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
