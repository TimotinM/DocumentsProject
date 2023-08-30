using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.Property(n => n.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(m => m.Macro)
                .WithMany(n => n.Micro)
                .UsingEntity(j => j
                    .ToTable("DocumentsTypeIerarchy"));
        }
    }
}
