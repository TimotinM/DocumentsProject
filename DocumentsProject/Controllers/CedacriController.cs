#nullable disable
using Application.Documents.Commands.CreateDocument;
using Application.Documents.Commands.UpdateDocument;
using Application.Documents.Queries.GetDocumentById;
using Application.Documents.Queries.GetDocumentsTable;
using Application.Documents.Queries.GetDocumentsTree;
using Application.Documents.Queries.GetDocumentTypeList;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Projects.Commands.CreateProject;
using Application.Projects.Commands.UpdateProject;
using Application.Projects.Queries.GetProjectDetails;
using Application.Projects.Queries.GetProjectDropDownList;
using Application.Projects.Queries.GetProjectsTable;
using Application.Projects.Queries.GetProjectsTree;
using Application.Projects.Queries.GetUpdateProject;
using Application.Responses;
using Application.Responses.JsTree;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.TableParameters;

namespace DocumentsProject.Controllers
{
    [Authorize]
    public class CedacriController : Controller
    {
        private readonly IMediator _mediator;

        public CedacriController(IMediator mediator)
        {
            _mediator = mediator;
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
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            return View("_CreateDocumentForm");
        }

        [HttpGet]
        public async Task<ActionResult> GetDocumentsTypeMicro(int macroId)
        {
            var response = await _mediator.Send(new GetDocumentsTypeMicroDropDownListRequest() { IdMacro = macroId });
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetInstitutionProjects(int institutionId)
        {
            var response = await _mediator.Send(new GetProjectDropDownListRequest() { InstitutionId = institutionId});
            return Ok(response);
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

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateDocument([FromForm] CreateDocumentDto request)
        {
            var response = await _mediator.Send(new CreateDocumentCommand() { DocumentDto = request });

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetUpdateDocument(int documentId)
        {
            var model = await _mediator.Send(new GetUpdateDocument() { Id = documentId });
            ViewBag.Projects = await _mediator.Send(new GetProjectDropDownListRequest() { InstitutionId = (int)model.IdInstitution });
            ViewBag.Micro = await _mediator.Send(new GetDocumentsTypeMicroDropDownListRequest() { IdMacro = model.IdMacro });
            ViewBag.Macro = await _mediator.Send(new GetDocumentsTypeDropDownListRequest() { IsMacro = true });
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            return PartialView("_UpdateDocumentForm", model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateDocument(int documentId, [FromForm] UpdateDocumentDto request)
        {
            request.Id = documentId;
            var response = await _mediator.Send(new UpdateDocumentRequest() { DocumentDto = request });
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

        [HttpGet]
        public async Task<ActionResult> GetProjectDetails(int projectId)
        {
            var model = await _mediator.Send(new GetProjectDetailsRequest() { Id = projectId });
            return PartialView("_ProjectDetails", model);
        }

        [HttpGet]
        public async Task<ActionResult> GetUpdateProject(int projectId)
        {
            ViewBag.Institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            var model = await _mediator.Send(new GetUpdateProjectRequest() { Id = projectId });            
            return PartialView("_UpdateProjectForm", model);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateProject(int projectId, [FromForm] UpdateProjectDto request)
        {
            request.Id = projectId;
            var response = await _mediator.Send(new UpdateProjectCommand() { ProjectDto =  request });

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
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
            var response = await _mediator.Send(new CreateProjectCommand() { Project = request });

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }

    }
}
