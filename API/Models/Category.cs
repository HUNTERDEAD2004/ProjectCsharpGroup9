namespace ProjectCsharpGroup9.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
