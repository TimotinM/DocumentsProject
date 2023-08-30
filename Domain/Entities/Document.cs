using Domain.Common;

namespace Domain.Entities
{
    public class Document : BaseAuditableEntity
    {
        public int IdInstitution { get; set; }
        public int IdUser { get; set; }
        public int IdType { get; set; }
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string SavePath { get; set; }
        public DateTime UploadDate { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime GroupingDate { get; set; }
        
    }
}
