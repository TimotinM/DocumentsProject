using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class InstitutionConfiguration : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.Property(i => i.InstCode)
                    .HasMaxLength(5)
                    .IsRequired();

            builder.Property(i => i.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.AdditionalInfo)
                .HasMaxLength(1000);
        }
    }
}
