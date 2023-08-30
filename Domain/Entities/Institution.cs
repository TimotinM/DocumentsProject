using Domain.Common;

namespace Domain.Entities
{
    public class Institution : BaseAuditableEntity
    {
        public string InstCode { get; set; }
        public string Name { get; set; }
        public string? AdditionalInfo { get; set; }

        public IList<Document>? Documents { get; set; }
        public IList<Project>? Projects { get; set; }
        public IList<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
