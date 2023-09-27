using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<BaseCommandResponse>
    {
        public UpdateProjectDto ProjectDto { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthService _authService; 

        public UpdateProjectCommandHandler(IApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<BaseCommandResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateProjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProjectDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Edit Project Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var project = await _context.Projects
                    .SingleAsync(x => x.Id == request.ProjectDto.Id);

                project.Name = request.ProjectDto.Name;
                project.DateTill = request.ProjectDto.DateTill;
                project.AdditionalInfo = request.ProjectDto.AdditionalInfo;
                project.IsActive = request.ProjectDto.IsActive;
                project.ApplicationUser = await _authService.GetCurrentUserAsync();

                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Project was edited successful";
            }

            return response;
        }
    }
}
