using Application.DTOs.Institution;
using Application.DTOs.Institution.Validators;
using Application.DTOs.User;
using Application.Istitutions.Requests.Commands;
using Application.Istitutions.Requests.Queris;
using Application.Responses;
using Application.Users.Requests.Conmmand;
using Application.Users.Requests.Queris;
using DocumentsProject.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

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

        [HttpGet]
        public async Task<ActionResult<List<InstitutionDto>>> GetInstitutionList()
        {
            var request = new GetInstitutionsRequest();
            var institutions = await _mediator.Send(request);

            return Json(new { data = institutions });
        }

        [HttpGet]
        public async Task<ActionResult> GetCreateInstitution(List<string> errors = null)
        {
            ViewBag.Errors = errors;
            return View("_CreateInstitutionForm");
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateInstitution([FromForm] CreateInstitutionDto request)
        {
                var command = new CreateInstitutionCommand() { InstitutionDto = request };
                var response = await _mediator.Send(command);
                ViewBag.Errors = response.Errors;
                return response;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersListDto>>> GetUsers()
        {
            var request = new GetUsersRequest();
            var users = await _mediator.Send(request);
            return Json(new {data = users});
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> CreateUser([FromForm] CreateUserDto request)
        {
            var command = new CreateUserCommand() { UserDto = request };
            var response = await _mediator.Send(command);
            return Json(response);
        }
    }
}
