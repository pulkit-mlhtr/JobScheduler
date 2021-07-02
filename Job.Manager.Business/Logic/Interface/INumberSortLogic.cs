using Job.Manager.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job.Manager.Business.Logic.Interface
{
    public interface INumberSortLogic
    {
        /// <summary>
        /// Get all the jobs 
        /// </summary>
        /// <returns></returns>
        Task<IList<NumberArraySort>> GetAll();

        /// <summary>
        /// Get all the jobs pending in the queue
        /// </summary>
        /// <returns></returns>
        Task<IList<NumberArraySort>> GetAllPending();

        /// <summary>
        /// Get all the completed jobs
        /// </summary>
        /// <returns></returns>
        Task<IList<NumberArraySort>> GetAllCompleted();

        /// <summary>
        /// Get all the inprocess jobs
        /// </summary>
        /// <returns></returns>
        Task<IList<NumberArraySort>> GetAllInProcesss();

        /// <summary>
        /// Get specific job by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<NumberArraySort> Get(Guid Id);

        /// <summary>
        /// Remove job by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool Delete(NumberArraySort job);

        /// <summary>
        /// Add new job
        /// </summary>
        /// <param name="job"></param>
        Task<JobDetails> Add(JobDetails job);

        /// <summary>
        /// Update the job
        /// </summary>
        /// <param name="job"></param>
        Task Update(JobDetails job);
    }
}
