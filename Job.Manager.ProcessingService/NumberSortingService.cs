using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Job.Manager.ProcessingService
{
    public class NumberSortingService : IHostedService, IDisposable
    {
        private readonly Subscriber<JobDetails> _subscriber;
        private readonly IServiceScopeFactory _scopeFactory;

        public NumberSortingService(Subscriber<JobDetails> subscriber,
                                    IServiceScopeFactory scopeFactory)
        {
            _subscriber = subscriber;
            _scopeFactory = scopeFactory;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Publisher.DataPublisher += ExexuteNumberSortJob;

        }

        private void ExexuteNumberSortJob(object sender, MessageArgs<JobDetails> e)
        {
            Task.Run(async () =>
            {
                await GetNumberArrayAndSort(e.Message);

            });
        }

        private async Task GetNumberArrayAndSort(JobDetails message)
        {
            var job = (NumberArraySort)message;

            var numberArray = job.InputArray.Split(",")
                                             .ToList()
                                             .ConvertAll(c => Convert.ToInt32(c)).Distinct()
                                             .ToArray();

            UpdateJob(job, JobStatus.InProcess.ToString(), false, true, null);
            numberArray = SortQuick(numberArray, 0, numberArray.Length - 1);
            UpdateJob(job, JobStatus.Completed.ToString(), true, false, string.Join(",", numberArray));

        }

        private void UpdateJob(NumberArraySort job, string jobStatus, bool IsComplete, bool IsStarted, string outputArray)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var numberLogic = scope.ServiceProvider.GetRequiredService<INumberSortLogic>();

                if (jobStatus == JobStatus.InProcess.ToString() && IsStarted)
                {
                    job.Status = JobStatus.InProcess.ToString();
                    job.StartTime = DateTime.Now;
                }
                if (jobStatus == JobStatus.Completed.ToString() && IsComplete)
                {
                    job.Status = JobStatus.Completed.ToString();
                    job.EndTime = DateTime.Now;
                    job.OutputArray = outputArray;
                }


                numberLogic.Update(job);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int[] SortQuick(int[] arr, int left, int right)
        {
            int[] sortedList;
            sortedList = arr;
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    sortedList = SortQuick(arr, left, pivot - 1);
                }

                if (pivot + 1 < right)
                {
                    sortedList = SortQuick(arr, pivot + 1, right);
                }
            }

            return sortedList;
        }

        private int Partition(int[] numbers, int left, int right)
        {
            int pivot = numbers[left];

            while (true)
            {
                if (right == 2 && left == 1)
                { }

                while (numbers[left] < pivot)
                {
                    left++;
                }

                while (numbers[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    int temp = numbers[right];

                    numbers[right] = numbers[left];

                    numbers[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
