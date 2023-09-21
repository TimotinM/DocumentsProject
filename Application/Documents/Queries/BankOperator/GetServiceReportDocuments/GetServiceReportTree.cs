using Application.Common.Interfaces;
using Application.Responses.JsTree;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Documents.Queries.BankOperator.GetServiceReportDocuments
{
    public class GetServiceReportTreeRequest : IRequest<List<JsTree>>
    {
        public int IdUser { get; set; }
    }

    public class GetServiceReportTreeRequestHandler : IRequestHandler<GetServiceReportTreeRequest, List<JsTree>>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetServiceReportTreeRequestHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<JsTree>> Handle(GetServiceReportTreeRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.IdUser.ToString());
            var institutionId = user.IdInstitution;

            var response = await _context.Documents                   
                    .Include(x => x.DocumentType).ThenInclude(m => m.Macro)
                    .Where(x => x.DocumentType.Macro.FirstOrDefault().Name == "Service" && x.IdInstitution == institutionId)
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
                            }).ToList()
                    }).ToListAsync();

            return response;
        }
    }
}
