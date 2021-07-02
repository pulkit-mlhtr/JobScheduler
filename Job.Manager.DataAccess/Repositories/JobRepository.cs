using Job.Manager.DataAccess.Common;
using Job.Manager.DataAccess.Database;

namespace Job.Manager.DataAccess.Repositories
{
    public class JobRepository : Repository<NumberSortJobs>
    {
        private JobsContext _dbContext;

        public JobRepository(JobsContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
