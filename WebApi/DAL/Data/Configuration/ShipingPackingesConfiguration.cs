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
    public class ShipingPackingesConfiguration : IEntityTypeConfiguration<ShipingPackging>
    {
        public void Configure(EntityTypeBuilder<ShipingPackging> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ShipingPackgingAname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ShipingPackgingAname");
            entity.Property(e => e.ShipingPackgingEname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ShipingPackgingEname");

        }
    }
}
