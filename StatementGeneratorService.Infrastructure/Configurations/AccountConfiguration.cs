using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);


            builder.Property(x => x.AccountNumber)
                .HasMaxLength(50)
                .IsRequired();


            builder.HasIndex(x => x.AccountNumber)
                .IsUnique();


            builder.Property(x => x.Balance)
                .HasPrecision(18, 2);

            builder.Property(x => x.AccountType)
                .HasConversion<int>();


            builder.Property(x => x.Status)
                .HasConversion<int>();


            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Statements)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
