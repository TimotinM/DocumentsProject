using Application.Istitutions.Commands.CreateInstitution;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Istitutions.Queries.GetInstitutionsTable;
using Application.Responses;
using Application.Users.Conmmand.ChangeUserPassword;
using Application.Users.Conmmand.CreateUser;
using Application.Users.Conmmand.UpdateUser;
using Application.Users.Queris;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.TableParameters;

namespace DocumentsProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateInstitutionDto> _validator;

        public AdminController(IMediator mediator, IValidator<CreateInstitutionDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        public IActionResult Index()
        {
            return View();
        }    

        [HttpPost]
        public async Task<ActionResult> GetInstitutionList(DataTablesParameters dataTablesParameters = null)
        {
            var request = new GetInstitutionsRequest() { Parameters = dataTablesParameters };
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public ActionResult GetCreateInstitution(List<string> errors = null)
        {
            ViewBag.Errors = errors;
            return PartialView("_CreateInstitutionForm");
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateInstitution([FromForm] CreateInstitutionDto request)
        {
                var command = new CreateInstitutionCommand() { InstitutionDto = request };
                var response = await _mediator.Send(command);
                ViewBag.Errors = response.Errors;
                return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> GetUsers(DataTablesParameters dataTablesParameters = null)
        {
            var request = new GetUsersRequest() { Parameters = dataTablesParameters };
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetCreateUser()
        {
            var request = new GetInstitutionDropDownListRequest();
            var institutions = await _mediator.Send(request);
            ViewBag.Institutions = institutions;
            return PartialView("_AddUserForm");
        }

        [HttpGet]
        public ActionResult GetChangeUserPassword(int id, List<string> errors = null)
        {
            ViewBag.UserId = id;
            ViewBag.Errors = errors;
            return PartialView("_ChangeUserPasswordForm");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateUser([FromForm] CreateUserDto request)
        {
            var command = new CreateUserCommand() { UserDto = request };
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> UpdateUserEnabled(int id)
        {
            var command = new UpdateUserEnabledCommand() { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<BaseCommandResponse>> ChangeUserPassword(int userId, [FromForm] ChangeUserPasswordDto request)
        {
            request.Id = userId;
            var command = new ChangeUserPasswordCommand() { UserPassword = request };
            var response = await _mediator.Send(command);
            if (!response.Success)             
                return BadRequest(response.Errors);
            return Ok(response);
        }
     
    }
}
