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
            var orderColumn = request.Parameters.Columns[request.Parameters.Order[0].Column].Name;
            var toFind = request.Parameters.Search.Value ?? "";

            var query = _context.Documents
                .Include(x => x.Institution)
                .Include(x => x.Project)
                .Include(x => x.DocumentType).ThenInclude(x => x.Macro)
                .Where(x => x.Name.Contains(toFind)
                    || x.DocumentType.Name.Contains(toFind)
                    || x.DocumentType.Macro.Any(m => m.Name.Contains(toFind))
                    || x.Institution.Name.Contains(toFind)
                    || x.Project.Name.Contains(toFind)
                    || x.GroupingDate.Year.ToString().Contains(toFind));

            var total = await query.CountAsync();

            var result = await query
                .OrderByExtension(orderColumn, request.Parameters.Order[0].Dir)
                .Skip(request.Parameters.Start)
                .Take(request.Parameters.Length)
                .Select(x => new DocumentsTableDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    DocumentType = x.DocumentType.IsMacro ?
                        x.DocumentType.Name :
                        x.DocumentType.Macro.FirstOrDefault().Name + "/" + x.DocumentType.Name,
                    Institution = x.Institution.Name,
                    GroupingDate = x.GroupingDate,
                    Project = x.Project.Name
                })
                .ToListAsync();

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
