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
    public class ShipingTypeConfiguration : IEntityTypeConfiguration<ShipingType>
    {
        public void Configure(EntityTypeBuilder<ShipingType> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.Property(e => e.ShipingTypeAname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeAName");
            entity.Property(e => e.ShipingTypeEname)
                .HasMaxLength(200)
                .HasColumnName("ShippingTypeEName");
        }
    }
}
