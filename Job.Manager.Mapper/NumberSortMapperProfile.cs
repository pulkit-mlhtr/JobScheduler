using AutoMapper;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using System;

namespace Job.Manager.Mapper
{
    public class NumberSortMapperProfile : Profile
    {
        public NumberSortMapperProfile()
        {
            CreateMap<NumberSortJobs, NumberArraySort>()
                .ForMember(dest =>
            dest.EnqueueDate,
            opt => opt.MapFrom(src => src.EnqueueDate))
              .ForMember(dest =>
            dest.JobId,
            opt => opt.MapFrom(src => src.Id))
              .ForMember(dest =>
              dest.ExecDurationInMs,
              opt => opt.MapFrom(src => CalculateDuration(src.StartTime, src.EndTime)))
              .ForMember(dest =>
              dest.Status,
              opt => opt.MapFrom(src => GetStatus(src.IsComplete, src.InPending, src.InProcess)))              
              .ForCtorParam("inputdata",
              opt => opt.MapFrom(src => src.InputArray));

            CreateMap<NumberArraySort, NumberSortJobs>()
               .ForMember(dest =>
           dest.EnqueueDate,
           opt => opt.MapFrom(src => src.EnqueueDate))
             .ForMember(dest =>
           dest.Id,
           opt => opt.MapFrom(src => src.JobId))
             .ForMember(dest =>
             dest.InPending,
             opt => opt.MapFrom(src => src.Status == JobStatus.Pending.ToString()))
             .ForMember(dest =>
             dest.InProcess,
             opt => opt.MapFrom(src => src.Status == JobStatus.InProcess.ToString()))
             .ForMember(dest =>
             dest.IsComplete,
             opt => opt.MapFrom(src => src.Status == JobStatus.Completed.ToString()))
             .ForMember(dest =>
              dest.OutputArray,
              opt => opt.MapFrom(src => src.OutputArray));

        }

        private string GetStatus(bool isComplete, bool inPending, bool inProcess)
        {
            if (isComplete)
            {
                return JobStatus.Completed.ToString();
            }
            if (inProcess)
            {
                return JobStatus.InProcess.ToString();
            }
            if (inPending)
            {
                return JobStatus.Pending.ToString();
            }

            return JobStatus.Pending.ToString();
        }

        private float CalculateDuration(DateTime? startTime, DateTime? endTime)
        {
            if (startTime.HasValue && endTime.HasValue)
            {
                float duration = (float)(endTime - startTime).Value.TotalMilliseconds;
                return duration > 0 ? duration : 0;
            }
            else
            {
                return (float)0;
            }
        }
    }
}
