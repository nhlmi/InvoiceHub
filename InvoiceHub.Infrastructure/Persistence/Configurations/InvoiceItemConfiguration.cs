using InvoiceHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceHub.Infrastructure.Persistence.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.OwnsOne(x => x.Price, money =>
        {
            money.Property(p => p.Amount)
                .HasColumnName("Amount");

            money.Property(p => p.Currency)
                .HasColumnName("Currency")
                .HasMaxLength(10);
        });
    }
}