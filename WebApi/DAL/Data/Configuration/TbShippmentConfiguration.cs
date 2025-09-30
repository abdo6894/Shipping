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
    public class TbShippmentConfiguration : IEntityTypeConfiguration<TbShippment>
    {
        public void Configure(EntityTypeBuilder<TbShippment> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.PackageValue).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.ShippingRate).HasColumnType("decimal(8, 4)");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("FK_TbShippments_TbPaymentMethods");

            entity.HasOne(d => d.Receiver).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserReceivers");

            entity.HasOne(d => d.Sender).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbUserSebders");

            entity.HasOne(d => d.ShippingType).WithMany(p => p.TbShippments)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbShippments_TbShippingTypes");
        }
    }
}
