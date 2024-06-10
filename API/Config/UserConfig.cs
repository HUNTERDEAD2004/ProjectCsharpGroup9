using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.UserID);
            builder.HasOne(p=>p.Role).WithMany(p=>p.Users).HasForeignKey(p=>p.RoleID);
            builder.Property(p=>p.Address).IsUnicode(true).IsFixedLength(true).HasMaxLength(256);
        }
    }
}
