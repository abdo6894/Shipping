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
    public class TbCityConfiguration : IEntityTypeConfiguration<TbCity>
    {
        public void Configure(EntityTypeBuilder<TbCity> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CityAname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CityAName");
            entity.Property(e => e.CityEname)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CityEName");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TbCities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TbCities_TbCountries");
        }
    }
}
