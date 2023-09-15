using Application.Documents.Commands.CreateDocument;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Responses;
using Application.Users.Conmmand.ChangeUserPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetCreateDocumentAsync()
        {
            var institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            ViewBag.Macro = institutions;
            ViewBag.Micro = institutions;
            ViewBag.Project = institutions;
            ViewBag.Institutions = institutions;
            return PartialView("_CreateDocumentForm");
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateDocument([FromForm] CreateDocumentDto request)
        {
            var command = new CreateDocumentCommand() { DocumentDto = request };
            var response = await _mediator.Send(command);

            if (!response.Success)
                return BadRequest(response.Errors);
            return Ok(response);
        }
    }
}
