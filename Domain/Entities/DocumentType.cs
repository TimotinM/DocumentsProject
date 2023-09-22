using Domain.Common;

namespace Domain.Entities
{
    public class DocumentType : BaseAuditableEntity
    {
        public string? Code { get; set; }
        public string Name { get; set; }
        public bool IsMacro { get; set; }
        public string? TypeDscr { get; set; }
        public bool IsDateGrouped { get; set; }

        public IList<Document> Documents { get; set; } = new List<Document>();

        public IList<DocumentType> Macro { get; set; } = new List<DocumentType>();
        public IList<DocumentType> Micro { get; set; } = new List<DocumentType>();
    }
}
