using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FamilyDemoAPIv2.Service;
using FamilyDemoAPIv2.DBContext;
using AutoMapper;
using Newtonsoft.Json.Serialization;

namespace FamilyDemoAPIv2
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
            services.AddControllers();

            #region // Configure controllers, output formatters / serializers and 500 error handling.
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

                #region // Add response error codes to API documentation at controller level.
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status201Created));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status204NoContent));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                #endregion

                //TODO: Add caching at controller level  => Startup.cs.

                // XML output serializer.
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });
            //TODO: Add 500 internal server error handling => Startup.cs.
            #endregion

            #region // Register services.
            // Register Automapper.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register FamilyDemoAPIv2Repository service.
            services.AddScoped<IFamilyDemoAPIv2Repository, FamilyDemoAPIv2Repository>();
            #endregion

            #region // Configure database connection / context.
            services.AddDbContext<FamilyDemoAPIv2Context>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("connectionString"));
            });
            #endregion

            #region // Register Swagger service witin middleware.
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "LibraryOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Library API",
                        Version = "1",
                        Description = "Access, update and create family members and their beloved pets ::))",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "duanebillue@yahoo.com",
                            Name = "Duane Billue",
                            Url = new Uri("https://www.linkedin.com/in/duanebillue/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                // Integrate XML documentation with Swagger.
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
            #endregion
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

            #region // Configure Swagger for pipeline
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                        "/swagger/LibraryOpenAPISpecification/swagger.json",
                        "FamilyDemo API v2");
                setupAction.RoutePrefix = "";
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
