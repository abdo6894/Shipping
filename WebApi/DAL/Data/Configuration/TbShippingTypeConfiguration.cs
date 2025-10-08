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
    public class TbShippingTypeConfiguration : IEntityTypeConfiguration<TbShippingType>
    {
        public void Configure(EntityTypeBuilder<TbShippingType> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime2");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime2");

            entity.Property(e => e.ShippingTypeAname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeAName");
            entity.Property(e => e.ShippingTypeEname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeEName");
        }
    }
}
