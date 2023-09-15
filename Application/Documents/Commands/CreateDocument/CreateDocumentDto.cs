using Microsoft.AspNetCore.Http;

namespace Application.Documents.Commands.CreateDocument
{
    public class CreateDocumentDto
    {
        public IFormFile SavePath { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime GroupingDate { get; set; }
        public int? IdInstitution { get; set; }
        public int IdUser { get; set; }
        public int IdMacro { get; set; }
        public int? IdMicro { get; set; }
        public int? IdProject { get; set; }
    }
}
