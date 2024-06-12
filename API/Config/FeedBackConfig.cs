using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class FeedBackConfig : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder.HasKey(p => p.FeedBackID);
            builder.HasOne(p => p.User).WithMany(p => p.FeedBacks).HasForeignKey(p => p.UserID);
            builder.HasOne(p => p.Product).WithMany(p => p.FeedBacks).HasForeignKey(p => p.ProductID);
        }
    }
}
