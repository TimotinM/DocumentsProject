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

        public CreateDocumentCommandHandler(IApplicationDbContext context, IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
                var bankName = _context.Institutions.Where(x => x.Id == request.DocumentDto.IdInstitution).Select(x => x.Name).FirstOrDefault();
                var year = request.DocumentDto.GroupingDate.Year.ToString();
                var month = request.DocumentDto.GroupingDate.Month.ToString();
                var documentName = request.DocumentDto.SavePath.FileName;
                var destination = bankName + "/" + year + "/" + month + "/" + documentName;
                var filePath = Path.Combine(_hostEnvironment.ContentRootPath, destination);

                await request.DocumentDto.SavePath.CopyToAsync(new FileStream(filePath, FileMode.Create));

                var entity = new Document()
                {
                    IdInstitution = request.DocumentDto.IdInstitution,
                    IdProject = request.DocumentDto.IdProject,
                    IdUser = request.DocumentDto.IdUser,
                    IdType = (int)(request.DocumentDto.IdMicro != null ? request.DocumentDto.IdMicro : request.DocumentDto.IdMacro),
                    AdditionalInfo = request.DocumentDto.AdditionalInfo,
                    UploadDate = DateTime.Now,
                    GroupingDate = request.DocumentDto.GroupingDate,
                    Name = request.DocumentDto.SavePath.Name,
                    SavePath = filePath
                };
            }
            return response;
        }
    }
}
