using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Job.Manager.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true).Build();
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.SQLite(Environment.CurrentDirectory + @"\JobsRepo.db", tableName: "Logs", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                .Filter.ByExcluding(e => e.MessageTemplate.Text.Contains("favicon", StringComparison.OrdinalIgnoreCase))
                .CreateLogger();
            try
            {
                Log.Information("Application starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed", ex.StackTrace);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureAppConfiguration(opt => opt.AddConfiguration(Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
