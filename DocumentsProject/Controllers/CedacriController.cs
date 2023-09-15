using Application.Documents.Commands.CreateDocument;
using Application.Documents.Queries.GetDocumentTypeList;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocumentsProject.Controllers
{
    [Authorize]
    public class CedacriController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CedacriController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
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
        public async Task<ActionResult> GetCreateDocumentAsync()
        {
            ViewBag.Macro = await _mediator.Send(new GetDocumentsTypeDropDownListRequest() { IsMacro = true });
            ViewBag.Project = await _mediator.Send(new GetDocumentsTypeDropDownListRequest() { IsMacro = false });
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            return PartialView("_CreateDocumentForm");
        }

        [HttpGet] 
        public async Task<ActionResult> GetDocumentsTypeMicro(int macroId)
        {
            var response = await _mediator.Send(new GetDocumentsTypeMicroDropDownListRequest() { IdMacro = macroId });
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateDocument([FromForm] CreateDocumentDto request)
        {
            request.IdUser = Int32.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var command = new CreateDocumentCommand() { DocumentDto = request };
            var response = await _mediator.Send(command);

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }
    }
}
