using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Institution> Institutions { get; }
    DbSet<Document> Documents { get; }
    DbSet<Project> Projects { get; }
    DbSet<DocumentType> DocumentTypes { get; }
    DbSet<DocumentTypeIerarchy> DocumentsTypeIerarchy { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
