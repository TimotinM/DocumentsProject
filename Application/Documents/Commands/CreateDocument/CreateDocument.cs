using Application.Common.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace Application.Documents.Commands.CreateDocument
{
    public class CreateDocumentCommand : IRequest<BaseCommandResponse>
    {
        public CreateDocumentDto DocumentDto { get; set; }
    }

    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IAuthService _authService;

        public CreateDocumentCommandHandler(IApplicationDbContext context, IHostEnvironment hostEnvironment, IAuthService authService)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _authService = authService;
        }

        public IHostEnvironment HostEnvironment { get; }

        public async Task<BaseCommandResponse> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDocumentDtoValidator(_context);
            var validationResult = await validator.ValidateAsync(request.DocumentDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Create Document Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var filePath = _hostEnvironment.ContentRootPath;
                var bankName = _context.Institutions.Where(x => x.Id == request.DocumentDto.IdInstitution).Select(x => x.Name).FirstOrDefault();
                var year = request.DocumentDto.GroupingDate.Year.ToString();
                var month = request.DocumentDto.GroupingDate.ToString("MMMM");

                var pathList = new List<string>() { "Documents", bankName, year, month };
                foreach (var path in pathList)
                {
                    filePath = Path.Combine(filePath, path);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);
                }

                var documentName = request.DocumentDto.SavePath.FileName;
                filePath = Path.Combine(filePath, documentName);

                using (var destinationStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.DocumentDto.SavePath.CopyToAsync(destinationStream);
                }

                var entity = new Document()
                {
                    IdInstitution = request.DocumentDto.IdInstitution,
                    IdProject = request.DocumentDto.IdProject,
                    ApplicationUser = await _authService.GetCurrentUserAsync(),
                    IdType = (int)(request.DocumentDto.IdMicro != null ? request.DocumentDto.IdMicro : request.DocumentDto.IdMacro),
                    AdditionalInfo = request.DocumentDto.AdditionalInfo,
                    UploadDate = DateTime.Now,
                    GroupingDate = request.DocumentDto.GroupingDate,
                    Name = request.DocumentDto.SavePath.FileName,
                    SavePath = filePath
                };

                var result = await _context.Documents.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Document was created successful";
            }
            return response;
        }
    }
}
