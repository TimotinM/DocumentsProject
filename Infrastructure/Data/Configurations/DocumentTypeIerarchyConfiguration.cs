using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class DocumentTypeIerarchyConfiguration : IEntityTypeConfiguration<DocumentTypeIerarchy>
    {
        public void Configure(EntityTypeBuilder<DocumentTypeIerarchy> builder)
        {
            builder.HasKey(k => new { k.IdMicro, k.IdMacro});

            builder.HasOne(d => d.Macro)
                .WithMany(dh => dh.DocumentsTypeIerarchyMacro)
                .HasForeignKey(d => d.IdMacro)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Micro)
               .WithMany(dh => dh.DocumentsTypeIerarchyMicro)
               .HasForeignKey(d => d.IdMicro)
               .OnDelete(DeleteBehavior.NoAction);
              
        }
    }
}
