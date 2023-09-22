using Application.Common.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<BaseCommandResponse>
    {
        public CreateProjectDto Project { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        public CreateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<BaseCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.Project);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Create Project Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var entity = new Project()
                {
                    Name = request.Project.Name,
                    DateFrom = request.Project.DateFrom,
                    DateTill = request.Project.DateTill,
                    IdInstitution = request.Project.IdInstitution,
                    AdditionalInfo = request.Project.AdditionalInfo,
                    IsActive = true,
                    IdUser = request.Project.IdUser,
                };

                await _context.Projects.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Project was created successful";
            }

            return response;
        }
    }
}
