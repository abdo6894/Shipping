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
    public class TbPaymentMethodConfiguration : IEntityTypeConfiguration<TbPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<TbPaymentMethod> entity)
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MethdAname)
                .HasMaxLength(200)
                .HasColumnName("MethdAName");
            entity.Property(e => e.MethodEname)
                .HasMaxLength(200)
                .HasColumnName("MethodEName");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        }
    }
}
