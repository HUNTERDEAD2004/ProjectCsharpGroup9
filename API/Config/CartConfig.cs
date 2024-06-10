using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(p => p.CartID);
            builder.HasOne(p => p.User).WithOne(p => p.Cart).HasForeignKey<Cart>(p => p.UserID); // 1 user - 1 cart
        }
    }
}
