using AutoMapper;
using FamilyDemoAPIv2.DBContext;
using FamilyDemoAPIv2.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

namespace FamilyDemoAPIv2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region // Cross Origin Resource Sharing
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost/15001");
                                  });
            });
            #endregion

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
                options.UseSqlServer(Configuration.GetConnectionString("connectionString_Azure"));
            });
            #endregion

            #region // Register Swagger service witin middleware.
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "LibraryOpenAPISpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = Configuration.GetSection("SwaggerDocumenation").GetSection("Title").Value.ToString(),
                        Version = Configuration.GetSection("SwaggerDocumenation").GetSection("APIVersion").Value.ToString(),
                        Description = Configuration.GetSection("SwaggerDocumenation").GetSection("Description").Value.ToString(),
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = Configuration.GetSection("SwaggerDocumenation").GetSection("Email").Value.ToString(),
                            Name = Configuration.GetSection("SwaggerDocumenation").GetSection("Name").Value.ToString(),
                            Url = new Uri(Configuration.GetSection("SwaggerDocumenation").GetSection("Url").Value.ToString())
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = Configuration.GetSection("SwaggerDocumenation").GetSection("LicenseName").Value.ToString(),
                            Url = new Uri(Configuration.GetSection("SwaggerDocumenation").GetSection("LicenseUrl").Value.ToString())
                        }
                    });;

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

            app.UseHsts();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

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
