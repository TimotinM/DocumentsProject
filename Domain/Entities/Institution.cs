using Domain.Common;

namespace Domain.Entities
{
    public class Institution : BaseAuditableEntity
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }

        public IList<Document>? Documents { get; set; } = new List<Document>();
        public IList<Project>? Projects { get; set; } = new List<Project>();
        public IList<ApplicationUser>? ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
