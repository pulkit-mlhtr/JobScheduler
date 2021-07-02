using Job.Manager.Business.Logic;
using Job.Manager.Business.Logic.Interface;
using Job.Manager.Business.Model;
using Job.Manager.DataAccess.Database;
using Job.Manager.DataAccess.Interface;
using Job.Manager.DataAccess.Repositories;
using Job.Manager.Mapper;
using Job.Manager.ProcessingService;
using Job.Manager.ProcessingService.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Job.Manager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<JobsContext>(options =>
            {
                options.UseSqlite("Filename=JobsRepo.db", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            });

            services.AddAutoMapper(typeof(NumberSortMapperProfile));
            services.AddTransient<IRepository<NumberSortJobs>, JobRepository>();
            services.AddSingleton<IPublisher<JobDetails>, Publisher<JobDetails>>();
            services.AddSingleton<Subscriber<JobDetails>, Subscriber<JobDetails>>();
            services.AddScoped<INumberSortLogic, NumberSortLogic>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddHostedService<NumberSortingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
