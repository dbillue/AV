using FamilyAPITestHarness.Services;
using FamilyAPITestHarness.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FamilyAPITestHarness
{
    class Program
    {
        private static IServiceCollection services { get; set; }
        static string APICallType = string.Empty;

        static async Task Main(string[] args)
        {
            APICallType = args[0].ToString();

            if(string.IsNullOrEmpty(APICallType)) return;

            Console.WriteLine("Begin {0} API calls", APICallType);

            IServiceCollection services = new ServiceCollection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ConfigureServices(services);

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            switch (APICallType)
            {
                case "Persons":
                    PersonController personController = new PersonController(configuration);
                    await personController.TestEntry();
                    break;
                case "Pets":
                    //TODO: Create controller to call PetService
                    break;
                default:
                    break;
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}
