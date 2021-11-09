using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using VaccReg.Services;
using VaccRegDb;

namespace VaccReg
{
    public class Startup
    {
        private const string swaggerVersion = "v1";
        private const string swaggerTitle = "VaccReg";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VaccRegContext>(options => options.UseSqlite(Configuration.GetConnectionString("registrationsDb")));

            services.AddScoped<RegistrationsService>();

            services.AddControllers();
            services.AddHostedService<DatabaseService>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(swaggerVersion, new OpenApiInfo
                {
                    Title = swaggerTitle,
                    Version = swaggerVersion
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Console.WriteLine("UseSwagger");
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint($"/swagger/{swaggerVersion}/swagger.json", swaggerTitle);
                });
            }

            app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthorization()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
