
using Domain.Common;

namespace Domain.Entities
{
    public class Project : BaseAuditableEntity
    {      
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool IsActive { get; set; }

        public int? IdInstitution { get; set; }
        public Institution? Institution { get; set; }

        public int IdUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IList<Document>? Documents { get; set; } = new List<Document>();
    }
}
