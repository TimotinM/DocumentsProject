
using Application.Common.Interfaces;
using Application.Responses.JsTree;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Documents.Queries.BankOperator.GetProjectReportDocuments
{
    public class GetProjectReportTreeRequest : IRequest<List<JsTree>>
    {
        public int IdUser { get; set; }
    }

    public class GetProjectReportTreeRequestHandler : IRequestHandler<GetProjectReportTreeRequest, List<JsTree>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetProjectReportTreeRequestHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<JsTree>> Handle(GetProjectReportTreeRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.IdUser.ToString());
            var institutionId = user.IdInstitution;

            var response = await _context.Documents
                    .Include(x => x.DocumentType).ThenInclude(m => m.Macro)
                    .Include(x => x.Project)
                    .Where(x => x.DocumentType.Macro.FirstOrDefault().Name == "Project" && x.IdInstitution == institutionId)
                    .GroupBy(doc => doc.Project)
                    .Select(projectGroup => new JsTree()
                    {
                        Text = projectGroup.Key.Name,
                        Children = projectGroup
                            .GroupBy(doc => doc.DocumentType.Name)
                                    .Select(docGroup => new JsTree()
                                    {
                                        Text = $"{docGroup.Key} ({docGroup.Count()})",
                                        Children = docGroup
                                            .Select(d => new JsTree()
                                            {
                                                Icon = "jstree-file",
                                                Text = d.Name,
                                                Value = d.Id
                                            }).ToList()
                                    }).ToList()
                    }).ToListAsync();

            return response;
        }
    }
}
