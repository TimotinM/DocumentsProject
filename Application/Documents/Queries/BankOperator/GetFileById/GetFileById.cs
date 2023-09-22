using Application.Common.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Documents.Queries.BankOperator.GetFileById
{
    public class GetFileByIdRequest : IRequest<FileResponse>
    {
        public int Id { get; set; }
    }

    public class GetFileByIdRequestHandler : IRequestHandler<GetFileByIdRequest, FileResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetFileByIdRequestHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FileResponse> Handle(GetFileByIdRequest request, CancellationToken cancellationToken)
        {
            var respone = new FileResponse();
            var file = await _context.Documents.FindAsync(request.Id);
            respone.FileStream = File.OpenRead(file.SavePath);
            respone.Succes = respone.FileStream != null ? true : false;
            respone.FileName = file.Name;
            respone.ContentType = "application/octet-stream";

            return respone;
        }
    }
}
