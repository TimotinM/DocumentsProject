using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(n => n.Name)
                .HasMaxLength(260)
                .IsRequired();

            builder.Property(a => a.AdditionalInfo)
                .HasMaxLength(1000);

        }
    }
}
