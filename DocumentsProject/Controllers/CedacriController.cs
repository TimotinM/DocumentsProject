using Application.Documents.Commands.CreateDocument;
using Application.Documents.Queries.BankOperator.GetServiceReportDocuments;
using Application.Documents.Queries.GetDocumentsTable;
using Application.Documents.Queries.GetDocumentsTree;
using Application.Documents.Queries.GetDocumentTypeList;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Queries.GetProjectDropDownList;
using Application.Projects.Queries.GetProjectsTable;
using Application.Projects.Queries.GetProjectsTree;
using Application.Responses;
using Application.Responses.JsTree;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.TableParameters;
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
        public async Task<ActionResult> GetCreateDocument()
        {
            ViewBag.Macro = await _mediator.Send(new GetDocumentsTypeDropDownListRequest() { IsMacro = true });
            ViewBag.Project = await _mediator.Send(new GetProjectDropDownListRequest());
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            return View("_CreateDocumentForm");
        }

        [HttpGet]
        public async Task<ActionResult<List<JsTree>>> GetDocumentsTree()
        {
            var response = await _mediator.Send(new GetDocumentsTreeRequest());
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> GetDocumentsTable(DataTablesParameters parameters = null)
        {
            var response = await _mediator.Send(new GetDocumentsTableRequest() { Parameters = parameters });
            return Ok(response);
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
            var response = await _mediator.Send(new CreateDocumentCommand() { DocumentDto = request });

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetCreateProject()
        {
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            return PartialView("_CreateProjectForm");
        }

        [HttpGet]
        public async Task<ActionResult<List<JsTree>>> GetProjectsTree()
        {
            var response = await _mediator.Send(new GetProjectsTreeRequest());
            return response;
        }

        [HttpPost]
        public async Task<ActionResult> GetProjectsTable(DataTablesParameters parameters = null)
        {
            var response = await _mediator.Send(new GetProjectsTableRequest() { Parameters = parameters });
            return Ok(response);    
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateProject([FromForm] CreateProjectDto request)
        {
            request.IdUser = Int32.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _mediator.Send(new CreateProjectCommand() { Project = request });

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }

    }
}
