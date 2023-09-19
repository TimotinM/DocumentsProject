using Application.Common;
using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.TableParameters;

namespace Application.Documents.Queries.GetDocumentsTable
{
    public class GetDocumentsTableRequest : IRequest<DataTablesResponse<DocumentsTableDto>>
    {
        public DataTablesParameters Parameters { get; set; }
    }

    public class GetDocumentsTableRequestHandler : IRequestHandler<GetDocumentsTableRequest, DataTablesResponse<DocumentsTableDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetDocumentsTableRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DataTablesResponse<DocumentsTableDto>> Handle(GetDocumentsTableRequest request, CancellationToken cancellationToken)
        {
            var orderColumn = request.Parameters.Columns[request.Parameters.Order[0].Column].Name == "type" ? "documenttype" : request.Parameters.Columns[request.Parameters.Order[0].Column].Name;
            var toFind = request.Parameters.Search.Value ?? "";

            var result = await _context.Documents
                .Include(x => x.Institution)
                .Include(x => x.Project)
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .Where(x => x.Name.Contains(toFind)
                    || x.DocumentType.Name.Contains(toFind)
                    || x.DocumentType.Macro.FirstOrDefault().Name.Contains(toFind)
                    || x.Institution.Name.Contains(toFind)
                    || x.Project.Name.Contains(toFind))
                .OrderByExtension(orderColumn, request.Parameters.Order[0].Dir)
                .Skip(request.Parameters.Start)
                .Take(request.Parameters.Length)
                .Select(x => new DocumentsTableDto()
                {
                    Name = x.Name,
                    Type = x.DocumentType.IsMacro ?
                           x.DocumentType.Name :
                           x.DocumentType.Macro.FirstOrDefault().Name + "/" + x.DocumentType.Name,
                    Institution = x.Institution.Name,
                    Date = x.GroupingDate,
                    Project = x.Project.Name
                })
                .ToListAsync();

            var total = await _context.Documents
                .Include(x => x.Institution)
                .Include(x => x.Project)
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .Where(x => x.Name.Contains(toFind)
                    || x.DocumentType.Name.Contains(toFind)
                    || x.DocumentType.Macro.FirstOrDefault().Name.Contains(toFind)
                    || x.Institution.Name.Contains(toFind)
                    || x.Project.Name.Contains(toFind))
                .CountAsync();

            var response = new DataTablesResponse<DocumentsTableDto>()
            {
                Draw = request.Parameters.Draw,
                RecordsFiltered = total,
                RecordsTotal = total,
                Data = result
            };

            return response;
        }
    }
}
