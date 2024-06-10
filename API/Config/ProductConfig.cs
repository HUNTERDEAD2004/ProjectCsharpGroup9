using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID);
            builder.HasOne(p => p.Brand).WithMany(p => p.Products).HasForeignKey(p => p.BrandID);
            builder.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
        }
    }
}
