using Elevator.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Repositories.Database;
using Repositories.Repositories;

namespace Elevator.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Elevator.Api", Version = "v1"});
            });

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<DatabaseContext>();

            services.AddScoped<ProjectsRepository>();

            services.AddScoped<ProjectsService>();

        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elevator.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //todo(likvidator): пока нет
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
