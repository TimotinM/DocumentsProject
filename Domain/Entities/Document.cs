using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Document : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string SavePath { get; set; }   
        public DateTime UploadDate { get; set; }
        public string? AdditionalInfo { get; set; }
        public DateTime GroupingDate { get; set; }

        public int? IdInstitution { get; set; }
        public Institution? Institution { get; set; }

        public int IdUser { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int IdType { get; set; }
        public DocumentType DocumentType { get; set; }

        public int? IdProject { get; set; }
        public Project? Project { get; set; }

    }
}
