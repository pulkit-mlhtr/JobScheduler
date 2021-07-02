using Microsoft.VisualStudio.TestTools.UnitTesting;
using Job.Manager.ProcessingService;
using System;
using System.Collections.Generic;
using System.Text;
using Job.Manager.Business.Model;
using Job.Manager.ProcessingService.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Job.Manager.Tests
{
    [TestClass()]
    public class ProcessingServiceTest
    { 
        private readonly Subscriber<JobDetails> _subscriber;
        private readonly IPublisher<JobDetails> _publisher;

        public ProcessingServiceTest()
        {
            _subscriber = DI.services.BuildServiceProvider().GetService<Subscriber<JobDetails>>();
            _publisher = DI.services.BuildServiceProvider().GetService<IPublisher<JobDetails>>();
        }

        [TestMethod()]
        public void QuickSortAlgoTest()
        {
            NumberSortingService service = new NumberSortingService(_subscriber, MockObject._scopeFactory.Object);

            int[] inputArray = {1,3,6,8,978,789,56432,679,468,1,5,80,7,5,3,57 };

            inputArray = inputArray.Distinct().ToArray();

            var expactedOutputArray = inputArray.Distinct().ToList().OrderBy(o => o);

            int[] actualOutputArray = service.SortQuick(inputArray, 0, inputArray.Length - 1);

           Assert.IsTrue(actualOutputArray.SequenceEqual(expactedOutputArray));
          
        }
    }
}
