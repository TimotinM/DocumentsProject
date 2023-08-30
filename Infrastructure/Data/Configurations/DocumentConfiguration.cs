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

            builder.HasOne(i => i.Institution)
                .WithMany(d => d.Documents)
                .HasForeignKey(i => i.IdInstitution);

            builder.HasOne(i => i.DocumentType)
                .WithMany(d => d.Documents)
                .HasForeignKey(i => i.IdType);

            builder.HasOne(i => i.ApplicationUser)
                .WithMany(d => d.Documents)
                .HasForeignKey(i => i.IdUser);

            builder.HasOne(i => i.Project)
                .WithMany(d => d.Documents)
                .HasForeignKey(i => i.IdProject);

            builder.Property(a => a.AdditionalInfo)
                .HasMaxLength(1000);

        }
    }
}
