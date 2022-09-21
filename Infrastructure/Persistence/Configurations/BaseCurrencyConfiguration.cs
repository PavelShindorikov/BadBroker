using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class BaseCurrencyConfiguration : IEntityTypeConfiguration<BaseCurrency>
    {
        public void Configure(EntityTypeBuilder<BaseCurrency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Date).IsRequired();
        }
    }
}