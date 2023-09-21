using Application.Common;

namespace Application.Projects.Queries.GetProjectsTable
{
    public class ProjectTableDto : BaseDto
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string Institution { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public string FormattedDateFrom
        {
            get
            {
                return DateFrom.ToString("yyyy/MM/dd");
            }
        }

        public string FormattedDateTill
        {
            get
            {
                return DateTill.ToString("yyyy/MM/dd");
            }
        }
    }
}
