using System;

#nullable disable

namespace Job.Manager.DataAccess.Database
{
    public partial class NumberSortJobs
    {
        public Guid Id { get; set; }
        public string InputArray { get; set; }
        public string OutputArray { get; set; }
        public DateTime EnqueueDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsComplete { get; set; }
        public bool InPending { get; set; }
        public bool InProcess { get; set; }
    }
}
