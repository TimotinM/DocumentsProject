using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DocumentsProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Institution> Institutions => Set<Institution>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
        public DbSet<DocumentTypeIerarchy> DocumentsTypeIerarchy => Set<DocumentTypeIerarchy>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}