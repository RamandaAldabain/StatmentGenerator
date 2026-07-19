using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Infrastructure.Configurations
{
    public class StatementConfiguration : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {


            builder.HasKey(x => x.Id);


            builder.Property(x => x.OpeningBalance)
                .HasPrecision(18, 2);


            builder.Property(x => x.TotalCredits)
                .HasPrecision(18, 2);


            builder.Property(x => x.TotalDebits)
                .HasPrecision(18, 2);


            builder.Property(x => x.ClosingBalance)
                .HasPrecision(18, 2);


            builder.Property(x => x.Status)
                .HasConversion<int>();


            builder.HasIndex(x => new
            {
                x.AccountId,
                x.Month,
                x.Year
            })
            .IsUnique();


            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Statement)
                .HasForeignKey(x => x.StatementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
