using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentType : BaseAuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsMacro { get; set; }
        public bool IsDateGrouped { get; set; }

        public IList<Document>? Documents { get; set; }

        public IList<DocumentType>? Macro { get; set; }
        public IList<DocumentType>? Micro { get; set; }
    }
}
