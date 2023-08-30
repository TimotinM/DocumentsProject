

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(n => n.UserName)
                .HasMaxLength(32)
                .IsRequired();

            builder.HasOne(i => i.Institution)
                .WithMany(u => u.ApplicationUsers)
                .HasForeignKey(i => i.IdInstitution);         

        }
    }
}
