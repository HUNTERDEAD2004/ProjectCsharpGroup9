namespace ProjectCsharpGroup9.Models
{
    public class Gallery
    {
        public Guid GalleryID { get; set; }
        public Guid ProductID { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public virtual Product? Product { get; set; }
    }
}
