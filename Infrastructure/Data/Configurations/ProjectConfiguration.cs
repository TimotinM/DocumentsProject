

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(n => n.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.AdditionalInfo)
                .HasMaxLength(1000);

            builder.HasOne(i => i.Institution)
                .WithMany(p => p.Projects)
                .HasForeignKey(i => i.IdInstitution);

            builder.HasOne(p => p.ApplicationUser)
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.IdUser);
        }
    }
}
