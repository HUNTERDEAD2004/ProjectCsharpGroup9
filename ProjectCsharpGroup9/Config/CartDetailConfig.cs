using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class CartDetailConfig : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(p => p.CartDetailID);
            builder.HasOne(p => p.Cart).WithMany(p => p.CartDetails).HasForeignKey(p => p.CartID);
            builder.HasOne(p => p.Product).WithMany(p => p.CartDetails).HasForeignKey(p => p.ProductID);
        }
    }
}
