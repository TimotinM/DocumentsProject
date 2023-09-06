
namespace Domain.Entities
{
    public class DocumentTypeIerarchy
    {
        public int IdMacro { get; set; }
        public int IdMicro { get; set; }

        public virtual DocumentType Macro { get; set; }
        public virtual DocumentType Micro { get; set; }
    }
}
