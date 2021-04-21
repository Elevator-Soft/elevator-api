using System;
using Elevator.Api.Configuration;
using Elevator.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Repositories.Database;
using Repositories.Repositories;
using Elevator.Api.Middlewares;
using Git;
using Microsoft.Extensions.Logging;

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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(true);
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Elevator.Api", Version = "v1"});
            });

            services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin",
                        options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                });

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<DatabaseContext>();

            services.AddScoped<ProjectRepository>();

            services.AddScoped<IProjectService, ProjectService>();

            services.AddLogging();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                    options =>
                    {
                        var jwtConfiguration = configuration.GetSection("Bearer").Get<JwtBearerConfiguration>();
                        options.Authority = jwtConfiguration.Authority;
                        options.Audience = jwtConfiguration.Audience;
                    });

            services.AddSingleton<HandleExceptionsMiddleware>();
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

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<HandleExceptionsMiddleware>();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
