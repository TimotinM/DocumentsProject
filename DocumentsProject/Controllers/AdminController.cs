using Application.DTOs.Institution;
using Application.DTOs.Institution.Validators;
using Application.DTOs.User;
using Application.Istitutions.Requests.Commands;
using Application.Istitutions.Requests.Queris;
using Application.Responses;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private IValidator<CreateInstitutionDto> _validator;

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInstitution([FromForm] CreateInstitutionDto request)
        {
                var command = new CreateInstitutionCommand() { InstitutionDto = request };
                var response = await _mediator.Send(command);
                ViewBag.Errors = response.Errors;
                return await GetCreateInstitution(response.Errors);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetUsersListDto>>> GetUsers()
        {
            var request = new GetUsersListDto();
            var users = await _mediator.Send(request);
            return Json(new {data = users});
        }
    }
}
