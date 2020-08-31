using FamilyAPITestHarness.Controllers;
using FamilyAPITestHarness.DBContext;
using FamilyAPITestHarness.Interfaces;
using FamilyAPITestHarness.Services;
using FamilyAPITestHarness.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Threading.Tasks;

namespace FamilyAPITestHarness
{
    public class Program
    {
        public static string entity { get; set; }
        public static string action { get; set; }
        public static string personId { get; set; }
        public static string petId { get; set; }

        static async Task Main(string[] args)
        {
            entity = args[0].ToString().Trim();
            action = args[1].ToString().Trim();

            if (args.Length > 2) personId = args[2].ToString().Trim();

            if (args.Length > 3) petId = args[3].ToString().Trim();

            if (string.IsNullOrEmpty(entity)) return;

            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine("Begin FamilyAPITestHarness call test");

            // Calls the Run method in App, which is replacing Main
            await serviceProvider.GetService<App>().TestEntity();

            Console.WriteLine("End FamilyAPITestHarness call test");
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // Call ConfigurationBuilder and setup appsettings.json
            var config = LoadConfiguration();

            #region // Configure database connection / context.
            services.AddDbContext<HarnessDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("connectionString_Azure"));
                options.EnableSensitiveDataLogging(true);
            });
            #endregion

            #region // Serilog service configuration
            // Setup Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(),
                                config["LogFileDirectory"] + "FamiyAPITestHarness.log",
                                rollingInterval: RollingInterval.Day,
                                retainedFileCountLimit: 15)
                .CreateLogger();

            Log.Information("Application starting - " + DateTime.Now.ToString());
            Log.Information("Machine Name - " + Environment.MachineName);
            Log.Information("Environment User - " + Environment.UserName);
            Log.Information("Environment Version - " + Environment.Version);

            Log.Information("entity: " + entity);
            Log.Information("action: " + action);
            if (!string.IsNullOrEmpty(personId)) Log.Information("personId: " + personId);
            if (!string.IsNullOrEmpty(personId)) Log.Information("petId: " + petId);
            #endregion

            #region // Register services
            // Register services and configuration(s)
            services.AddSingleton(config);
            services.AddTransient<IPersonController, PersonController>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IPetController, PetController>();
            services.AddTransient<IPetService, PetService>();
            services.AddTransient<IBirthStateController, BirthStateController>();
            services.AddTransient<IBirthStateService, BirthStateService>();
            services.AddTransient<IHarnessDBService, HarnessDBService>();
            services.AddTransient<App>();
            services.AddSingleton<IUtilties, Utilities>();
            #endregion

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
