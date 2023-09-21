
namespace Application.Documents.Queries.BankOperator.GetDocumentDetails
{
    public class DocumentsDetailsDto
    {
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime GroupingDate { get; set; }
        public string? Institution { get; set; }
        public string User { get; set; }
        public string Macro { get; set; }
        public string? Micro { get; set; }
        public string? Project { get; set; }
    }
}
