using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configuration
{
    public class TbShippmentStatusConfiguration : IEntityTypeConfiguration<TbShippmentStatus>
    {
        public void Configure(EntityTypeBuilder<TbShippmentStatus> entity)
        {
            entity.ToTable("TbShippmentStatus");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Carrier).WithMany(p => p.TbShippmentStatuses)
                .HasForeignKey(d => d.CarrierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippmentStatus_TbCarriers");

            entity.HasOne(d => d.Shippment).WithMany(p => p.TbShippmentStatuses)
                .HasForeignKey(d => d.ShippmentId)
                .HasConstraintName("FK_TbShippmentStatus_TbShippments");
        }
    }    
    
}
