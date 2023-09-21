using Application.Common.Interfaces;
using Application.Responses.JsTree;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Documents.Queries.BankOperator.GetSLAReportDocuments
{
    public class GetSLAReportTreeRequest : IRequest<List<JsTree>>
    {
        public int IdUser { get; set; }
    }

    public class GetSLAReportTreeRequestHandler : IRequestHandler<GetSLAReportTreeRequest, List<JsTree>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetSLAReportTreeRequestHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<JsTree>> Handle(GetSLAReportTreeRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.IdUser.ToString());
            var institutionId = user.IdInstitution;

            var response = await _context.Documents
                    .Include(x => x.DocumentType).ThenInclude(m => m.Macro)
                    .Where(x => x.DocumentType.Name == "SLA" && x.IdInstitution == institutionId)
                    .GroupBy(doc => doc.GroupingDate.Year)
                    .Select(yearGroup => new JsTree()
                    {
                        Text = yearGroup.Key.ToString(),
                        Children = yearGroup
                            .GroupBy(doc => doc.GroupingDate.Month)
                            .Select(monthGroup => new JsTree()
                            {
                                Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthGroup.Key),
                                Children = monthGroup
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
