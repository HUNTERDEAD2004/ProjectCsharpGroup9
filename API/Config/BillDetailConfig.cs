using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCsharpGroup9.Models;

namespace ProjectCsharpGroup9.Config
{
    public class BillDetailConfig : IEntityTypeConfiguration<BillDetail>
    {
        public void Configure(EntityTypeBuilder<BillDetail> builder)
        {
            builder.HasKey(p => p.BillDetailId);
            builder.HasOne(p => p.Bill).WithMany(p => p.BillDetails).HasForeignKey(p => p.BillId);
            builder.HasOne(p => p.Product).WithMany(p => p.BillDetails).HasForeignKey(p => p.ProductId);
        }
    }
}
