using Application.Common;

namespace Application.Documents.Queries.GetDocumentsTable
{
    public class DocumentsTableDto : BaseDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Institution { get; set; }
        public string Project { get; set; }
    }
}
