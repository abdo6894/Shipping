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
    public class TbSettingConfiguration : IEntityTypeConfiguration<TbSetting>
    {
        public void Configure(EntityTypeBuilder<TbSetting> entity)
        {
            entity.ToTable("TbSetting");
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
        }
    }
}
