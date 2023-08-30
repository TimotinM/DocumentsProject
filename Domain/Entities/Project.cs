
using Domain.Common;

namespace Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public int IdInstitution { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool IsActive { get; set; }

        public IList<Document>? Documents { get; set; }
    }
}
