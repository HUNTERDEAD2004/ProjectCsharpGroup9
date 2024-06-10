using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class MembershipConfig : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(p => p.MemberID);
            builder.HasOne(p => p.User).WithOne(p => p.Membership).HasForeignKey<User>(p => p.UserID);
        }
    }
}
