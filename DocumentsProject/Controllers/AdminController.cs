﻿#nullable disable
using Application.Istitutions.Commands.CreateInstitution;
using Application.Istitutions.Commands.UpdateInstitution;
using Application.Istitutions.Queries.GetInstitutionDropDownList;
using Application.Istitutions.Queries.GetInstitutionsTable;
using Application.Istitutions.Queries.GetUpdateInstitution;
using Application.Responses;
using Application.Users.Conmmand.ChangeUserPassword;
using Application.Users.Conmmand.CreateUser;
using Application.Users.Conmmand.UpdateUser;
using Application.Users.Queris;
using Application.Users.Queris.GetRolesDropDownList;
using Application.Users.Queris.GetUserDetails;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Application.TableParameters;

namespace DocumentsProject.Controllers
{
    [Authorize(Roles = "Administrator")]   
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateInstitutionDto> _validator;

        public AdminController(IMediator mediator, IValidator<CreateInstitutionDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<ActionResult> GetUpdateInstitution(int institutionId)
        {
            var model = await _mediator.Send(new GetUpdateInstitutionRequest() { Id = institutionId });
            return PartialView("_UpdateInstitutionForm", model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateInstitution(int institutionId, [FromForm] UpdateInstitutionDto request)
        {
            request.Id = institutionId;
            var response = await _mediator.Send(new UpdateInstitutionRequest() { InstitutionDto = request });
            if (!response.Success)
                return BadRequest(response.Errors);
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
            ViewBag.Roles = await _mediator.Send(new GetRolesDropDownListRequest());
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
            if(!response.Success)
                return BadRequest(response.Errors);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserEdit(int id)
        {
            var model = await _mediator.Send(new GetUserByIdRequest { Id = id });
            var institutions = await _mediator.Send(new GetInstitutionDropDownListRequest());
            ViewBag.Roles = await _mediator.Send(new GetRolesDropDownListRequest());
            ViewBag.Institutions = institutions;
            return PartialView("_EditUserForm", model);
        }


        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> UpdateUser(int userId, [FromForm] UpdateUserDto request)
        {
            request.Id = userId;
            var response = await _mediator.Send(new UpdateUserCommand() { User = request});
            if (!response.Success)
                return BadRequest(response.Errors);
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

        [HttpGet]
        public async Task<ActionResult> GetUserDetails(int id)
        {
            var model = await _mediator.Send(new GetUserDetailsRequest { Id = id });
            return PartialView("_UserDetailsForm", model);
        }

    }
}
