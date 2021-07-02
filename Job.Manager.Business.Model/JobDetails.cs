
using System;

namespace Job.Manager.Business.Model
{
    public abstract class JobDetails
    {
        public Guid JobId { get; set; }
        public DateTime EnqueueDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float ExecDurationInMs { get; set; }
        public string Status { get; set; }               
    }
}
