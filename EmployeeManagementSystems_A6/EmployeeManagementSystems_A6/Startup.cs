// Startup.cs
using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeManagementSystems_A6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton((serviceProvider) =>
            {
                var cosmosClient = new CosmosClient(Configuration["CosmosDb:Account"], Configuration["CosmosDb:Key"]);
                var databaseName = Configuration["CosmosDb:DatabaseName"];
                var containerName = "EmployeeContainer";
                var cosmosDbService = new CosmosDbService(cosmosClient, databaseName, containerName);
                return cosmosDbService;
            });

            services.AddSingleton<ExcelService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeManagementSystems_A6 v1");
                    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root URL
                });
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
