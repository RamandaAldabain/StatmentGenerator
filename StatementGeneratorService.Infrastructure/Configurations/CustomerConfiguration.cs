using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {

            builder.HasKey(x => x.Id);


            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();


            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Address)
            .HasMaxLength(400)
             .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<int>();


            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
