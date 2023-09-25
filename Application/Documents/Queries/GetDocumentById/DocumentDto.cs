
using Application.Common;

namespace Application.Documents.Queries.GetDocumentById
{
    public class DocumentDto : BaseDto
    {
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime GroupingDate { get; set; }
        public int? IdInstitution { get; set; }
        public int IdMacro { get; set; }
        public int? IdMicro { get; set; }
        public int? IdProject { get; set; }
    }
}
