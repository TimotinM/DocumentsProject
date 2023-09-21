using Application.Documents.Queries.BankOperator.GetDocumentDetails;
using Application.Documents.Queries.BankOperator.GetFileById;
using Application.Documents.Queries.BankOperator.GetServiceReportDocuments;
using Application.Responses.JsTree;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentsProject.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BankController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<List<JsTree>>> GetServiceReportDocuments()
        {
            var userId = Int32.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _mediator.Send(new GetServiceReportTreeRequest() { IdUser = userId});
            return response;
        }

        [HttpGet]
        public async Task<ActionResult> GetFileById(int id)
        {
            var result = await _mediator.Send(new GetFileByIdRequest() { Id = id });
            if (!result.Succes)
                return NotFound();
            return File(result.FileStream, result.ContentType, result.FileName);
        }

        [HttpGet]
        public async Task<ActionResult> GetDocumentDetails(int id)
        {
            var model = await _mediator.Send(new GetDocumentDetailsRequest() { Id = id });
            return PartialView("_DocumentDetails", model);
        }

    }
}
