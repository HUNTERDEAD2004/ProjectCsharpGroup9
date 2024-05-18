﻿namespace ProjectCsharpGroup9.Models
{
    public class Brand
    {
        public Guid BrandID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
