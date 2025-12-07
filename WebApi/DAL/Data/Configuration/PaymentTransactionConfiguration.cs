using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Data.Configuration
{
    public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            // Table name (اختياري)
            builder.ToTable("PaymentTransactions");

            // Primary key
            builder.HasKey(x => x.Id);

            // OrderId
            builder.Property(x => x.OrderId)
                   .IsRequired()
                   .HasMaxLength(100);

            // Amount (حل مشكلة Warning)
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)   // ← أهم جزء
                   .IsRequired();

            // Status
            builder.Property(x => x.Status)
                   .HasMaxLength(50)
                   .IsRequired();

            // Gateway
            builder.Property(x => x.Gateway)
                   .HasMaxLength(50)
                   .IsRequired();

            // CreatedAt
            builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
