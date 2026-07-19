using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Infrastructure.Configurations
{
    public class StatementTransactionConfiguration : IEntityTypeConfiguration<StatementTransaction>
    {
        public void Configure(EntityTypeBuilder<StatementTransaction> builder)
        {

            builder.HasKey(x => x.Id);


            builder.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();


            builder.Property(x => x.Debit)
                .HasPrecision(18, 2);


            builder.Property(x => x.Credit)
                .HasPrecision(18, 2);


            builder.Property(x => x.BalanceAfterTransaction)
                .HasPrecision(18, 2);
        }
    }
}
