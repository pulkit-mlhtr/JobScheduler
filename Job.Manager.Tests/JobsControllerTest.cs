using Job.Manager.Api.Controllers;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Job.Manager.ProcessingService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Job.Manager.Tests
{
    [TestClass]
    public class JobsControllerTest
    {
        private readonly INumberSortLogic _numberSortLogic;
        private readonly IPublisher<JobDetails> _publisher;
        private readonly ILogger<JobsController> _logger;
        private readonly IRepository<NumberSortJobs> _jobRepo;
        private readonly JobsController _jobs;


        public JobsControllerTest()
        {
            _numberSortLogic = DI.services.BuildServiceProvider().GetService<INumberSortLogic>();
            _publisher = DI.services.BuildServiceProvider().GetService<IPublisher<JobDetails>>();
            _logger = DI.services.BuildServiceProvider().GetService<ILogger<JobsController>>();
            _jobRepo = DI.services.BuildServiceProvider().GetService<IRepository<NumberSortJobs>>();
            _jobs = new JobsController(_numberSortLogic, _publisher, _logger);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var response = _jobs.GetAll().Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
            Assert.IsNotNull(responseVal.Value);
            
        }

        [TestMethod]
        public void GetTest_Ok()
        {
            var jobs = _numberSortLogic.GetAll().Result;

            var response = _jobs.Get(jobs.First().JobId).Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
            Assert.IsNotNull(responseVal.Value);
        }

        [TestMethod]
        public void GetTest_NotFound()
        {            
            var response = _jobs.Get(System.Guid.NewGuid()).Result;

            Assert.IsTrue(response is NotFoundObjectResult);

            var responseVal = (NotFoundObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 404);            
        }

        [TestMethod]
        public void Enqueue_Ok()
        {
            var job = new NumberArraySort (
                "1,4,7,90,6,45,89,345"
            );
            var response = _jobs.Enqueue(job).Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);

            var addedJob = responseVal.Value as NumberArraySort;

            Assert.IsNotNull(addedJob.JobId);
        }

        [TestMethod]
        public void Dequeue_NotFound()
        {           
            var response = _jobs.Dequeue(System.Guid.NewGuid()).Result;

            Assert.IsTrue(response is NotFoundObjectResult);

            var responseVal = (NotFoundObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 404);
        }

        [TestMethod]
        public void Dequeue_Ok()
        {
            var job = _numberSortLogic.GetAll().Result.First();
            var response = _jobs.Dequeue(job.JobId).Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
        }

        [TestMethod]
        public void GetAllPendingLogicTest()
        {
            var response = _jobs.GetAllPending().Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
        }

        [TestMethod]
        public void GetAllCompletedLogicTest()
        {
            var response = _jobs.GetAllPending().Result;
            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
        }

        [TestMethod]
        public void GetAllInProcessLogicTest()
        {
            var response = _jobs.GetAllPending().Result;

            Assert.IsTrue(response is OkObjectResult);

            var responseVal = (OkObjectResult)response;

            Assert.IsTrue(responseVal.StatusCode == 200);
        }
    }
}
