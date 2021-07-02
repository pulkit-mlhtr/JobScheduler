using Job.Manager.Api.Controllers;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Job.Manager.ProcessingService.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Job.Manager.Tests
{
    [TestClass]
    public class NumberLogicTest
    {
        private readonly INumberSortLogic _numberSortLogic;
        private readonly IPublisher<JobDetails> _publisher;
        private readonly ILogger<JobsController> _logger;
        private readonly IRepository<NumberSortJobs> _jobRepo;


        public NumberLogicTest()
        {
            _numberSortLogic = DI.services.BuildServiceProvider().GetService<INumberSortLogic>();
            _publisher = DI.services.BuildServiceProvider().GetService<IPublisher<JobDetails>>();
            _logger = DI.services.BuildServiceProvider().GetService<ILogger<JobsController>>();
            _jobRepo = DI.services.BuildServiceProvider().GetService<IRepository<NumberSortJobs>>();
        }
        [TestMethod]
        public void AddJobLogicTest()
        {
            var job = _numberSortLogic.Add(new NumberArraySort("4,1,5,9,45,678,234,23,778")).Result;

            Assert.IsNotNull(job.JobId);
        }

        [TestMethod]
        public void GetAllLogicTest()
        {
            var jobList = _numberSortLogic.GetAll().Result;

            Assert.IsNotNull(jobList);
            Assert.IsTrue(jobList.Count > 0);
        }

        [TestMethod]
        public void GetAllPendingLogicTest()
        {
            var jobList = _numberSortLogic.GetAllPending().Result;

            Assert.IsNotNull(jobList);
            Assert.IsTrue(jobList.Count >= 0);
            Assert.IsFalse(jobList.Any(x => x.Status != JobStatus.Pending.ToString()));
        }

        [TestMethod]
        public void GetAllCompletedLogicTest()
        {
            var jobList = _numberSortLogic.GetAllPending().Result;

            Assert.IsNotNull(jobList);
            Assert.IsTrue(jobList.Count >= 0);
            Assert.IsFalse(jobList.Any(x => x.Status != JobStatus.Completed.ToString()));
        }

        [TestMethod]
        public void GetAllInProcessLogicTest()
        {
            var jobList = _numberSortLogic.GetAllPending().Result;

            Assert.IsNotNull(jobList);
            Assert.IsTrue(jobList.Count >= 0);
            Assert.IsFalse(jobList.Any(x => x.Status != JobStatus.InProcess.ToString()));
        }

        [TestMethod]
        public void GetJobLogicTest()
        {
            var job = _numberSortLogic.GetAll().Result.OrderByDescending(o => o.EnqueueDate).FirstOrDefault();

            if (job != null)
            {
                var findJob = _numberSortLogic.Get(job.JobId).Result;

                Assert.IsNotNull(findJob);
            }
            else
            {
                AddJobLogicTest();

                var latestJob = _numberSortLogic.GetAll().Result.OrderByDescending(o => o.EnqueueDate).FirstOrDefault();

                var findJob = _numberSortLogic.Get(latestJob.JobId).Result;

                Assert.IsNotNull(findJob);

            }
        }

        [TestMethod]
        public void DeleteJobLogicTest()
        {
            bool isJobDeleted = false;

            var job = _numberSortLogic.GetAll().Result.OrderByDescending(o => o.EnqueueDate).FirstOrDefault();

            if (job != null)
            {
                isJobDeleted = _numberSortLogic.Delete(job);
                var findJob = _numberSortLogic.Get(job.JobId).Result;

                Assert.IsTrue(isJobDeleted);
                Assert.IsNull(findJob);
            }
        }
    }
}
