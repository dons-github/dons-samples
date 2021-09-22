using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleContact.Data;
using SampleContact.Data.Repositories;
using SampleContact.Data.Repositories.Contracts;
using SampleContact.Data.Services.Contracts;

namespace SampleContact
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

            RegisterRepositories(services);
            RegisterServices(services);

            services.AddSwaggerGen();
            services.AddFluentValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Sample Contact API");
                c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root 
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IContactService, ContactService>();
            // more services would be added here in a larger system
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IContactRepository, ContactRepository>();
            // more repositories would be added here in a larger system
        }
    }
}
