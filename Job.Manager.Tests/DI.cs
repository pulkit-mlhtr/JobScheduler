using Job.Manager.Api.Controllers;
using Job.Manager.Business.Logic;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Job.Manager.DataAccess.Repositories;
using Job.Manager.Mapper;
using Job.Manager.ProcessingService;
using Job.Manager.ProcessingService.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Job.Manager.Tests
{
    public static class DI
    {
        public static ServiceCollection services = new ServiceCollection();

        static DI()
        {
            services.AddDbContext<JobsContext>(options =>
            {
                options.UseSqlite("Filename=TestJobsRepo.db", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });
            services.AddLogging();
            services.AddAutoMapper(typeof(NumberSortMapperProfile));
            services.AddTransient<IRepository<NumberSortJobs>, JobRepository>();
            services.AddSingleton<IPublisher<JobDetails>, Publisher<JobDetails>>();
            services.AddSingleton<ILogger<JobsController>, Logger<JobsController>>();
            services.AddSingleton<Subscriber<JobDetails>, Subscriber<JobDetails>>();
            services.AddScoped<INumberSortLogic, NumberSortLogic>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddHostedService<NumberSortingService>();
        }
    }
}
