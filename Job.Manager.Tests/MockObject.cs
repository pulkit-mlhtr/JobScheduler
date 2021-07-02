using AutoMapper;
using Job.Manager.Api.Controllers;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Job.Manager.ProcessingService.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;


namespace Job.Manager.Tests
{
    public static class MockObject
    {
        public static Mock<IPublisher<JobDetails>> _publisher;
        public static Mock<INumberSortLogic> _numberlogic;
        public static Mock<IRepository<NumberSortJobs>> _jobRepository;
        public static readonly Mock<IMapper> _mapper;
        public static readonly Mock<ILogger<JobsController>> _logger;
        public static readonly Mock<IServiceScopeFactory> _scopeFactory;

        static MockObject()
        {
            _publisher = new Mock<IPublisher<JobDetails>>();
            _numberlogic = new Mock<INumberSortLogic>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<JobsController>>();
            _scopeFactory = new Mock<IServiceScopeFactory>();

        }
    }
}
