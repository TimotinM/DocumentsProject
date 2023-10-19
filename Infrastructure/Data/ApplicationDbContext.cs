using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DocumentsProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>, IApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Institution> Institutions => Set<Institution>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseAuditableEntity;

                if (entity.Created == default)
                {
                    entity.Created = DateTimeOffset.UtcNow;
                    entity.CreatedBy = currentUser;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.LastModified = DateTimeOffset.UtcNow;
                    entity.LastModifiedBy = currentUser;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}