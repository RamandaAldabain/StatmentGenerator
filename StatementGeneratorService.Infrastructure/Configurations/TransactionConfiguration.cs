using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.HasKey(x => x.Id);


            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);


            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();


            builder.Property(x => x.ReferenceNumber)
                .HasMaxLength(100)
                .IsRequired();


            builder.HasIndex(x => x.ReferenceNumber)
                .IsUnique();


            builder.Property(x => x.Type)
                .HasConversion<int>();
        }
    }
}
