using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class BillConfig : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => x.BillId);
            builder.HasOne(p=>p.User).WithMany(p=>p.Bills).HasForeignKey(p=>p.UserId);
        }
    }
}
