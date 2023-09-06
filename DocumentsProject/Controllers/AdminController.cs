using Application.DTOs.Institution;
using Application.DTOs.Institution.Validators;
using Application.Istitutions.Requests.Commands;
using Application.Istitutions.Requests.Queris;
using Application.Responses;
using DocumentsProject.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsProject.Controllers
{
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
        public ActionResult CreateInstitution(List<string> errors = null)
        {
            ViewBag.Errors = errors;
            return PartialView("_CreateInstitutionForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<BaseCommandResponse>> CreateInstitution([FromForm] CreateInstitutionDto request)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validationResult.Errors)
                    errors.Add(error.ErrorMessage);
                return CreateInstitution(errors);
            }
            var command = new CreateInstitutionCommand() { InstitutionDto = request };
            var response = await _mediator.Send(command);
            return Ok("");
        }
    }
}
