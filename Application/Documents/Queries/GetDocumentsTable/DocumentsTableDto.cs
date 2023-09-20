using Application.Common;

namespace Application.Documents.Queries.GetDocumentsTable
{
    public class DocumentsTableDto : BaseDto
    {
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public DateTime GroupingDate { get; set; }
        public string Institution { get; set; }
        public string Project { get; set; }

        public string FormattedGroupingDate
        {
            get
            {
                return GroupingDate.ToString("yyyy/MM/dd");
            }
        }
    }
}
