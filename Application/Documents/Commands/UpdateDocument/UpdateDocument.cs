
using Application.Common.Interfaces;
using Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Documents.Commands.UpdateDocument
{
    public class UpdateDocumentRequest : IRequest<BaseCommandResponse>
    {
        public UpdateDocumentDto DocumentDto { get; set; }
    }

    public class UpdateDocumentRequestHandler : IRequestHandler<UpdateDocumentRequest, BaseCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuthService _authService;
        public UpdateDocumentRequestHandler(IApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<BaseCommandResponse> Handle(UpdateDocumentRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDocumentDtoValidator(_context);
            var validationResult = await validator.ValidateAsync(request.DocumentDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Update Document Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var document = await _context.Documents
                    .FirstAsync(x => x.Id == request.DocumentDto.Id);

                document.IdInstitution = request.DocumentDto.IdInstitution;
                document.IdType = (int)(request.DocumentDto.IdMicro != null ? request.DocumentDto.IdMicro : request.DocumentDto.IdMacro);
                document.GroupingDate = request.DocumentDto.GroupingDate;
                document.AdditionalInfo = request.DocumentDto.AdditionalInfo;
                document.IdProject = request.DocumentDto.IdProject;
                document.ApplicationUser = await _authService.GetCurrentUserAsync();

                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Document was updated successful";
            }

            return response;
        }
    }
}
