using FamilyDemoAPIv2.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.IO;

namespace FamilyDemoAPIv2
{
    public class Program
    {
        // Configuration property. 
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {

            // Configure Serilog.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.File(new JsonFormatter(), Configuration["LogFileDirectory"] + "FamilyDemoAPIv2.Log")
                .CreateLogger();

            try
            {
                Log.Information("Application starting - " + DateTime.Now.ToString());
                Log.Information("Machine Name - " + Environment.MachineName);
                Log.Information("Environment Name - " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                Log.Information("Environment User - " + Environment.UserName);
                Log.Information("Environment Version - " + Environment.Version);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
