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
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(x => x.Id)
                   .HasDefaultValueSql("NEWID()");

            builder.Property(rt => rt.Token)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(rt => rt.ExpiresAt)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");


            builder.Property(x => x.CreatedBy)
        .HasDefaultValueSql("NEWID()");


            builder.Property(x => x.CurrentState)
                   .HasDefaultValue(1);

            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("TbRefreshTokens");
        }
    }

}
