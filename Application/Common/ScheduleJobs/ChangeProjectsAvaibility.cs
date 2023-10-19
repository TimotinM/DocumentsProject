using Application.Common.Interfaces;

namespace Application.Common.ScheduleJobs
{
    public class ChangeProjectsAvaibility
    {
        private readonly IApplicationDbContext _context;

        public ChangeProjectsAvaibility(IApplicationDbContext context)
        {
            _context = context;
        }

        public async void ChangeProjectsIsActive(CancellationToken cancellationToken)
        {
            var projects = _context.Projects;

            foreach (var project in projects)
            {
                if (project.IsActive && project.DateTill.Date < DateTime.Now.Date)
                {
                    project.IsActive = false;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
      
    }
}
