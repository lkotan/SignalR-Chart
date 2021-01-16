using Chart.API.Hubs;
using Chart.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chart.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSignalR();

            services.AddScoped<CovidService>();

            services.AddDbContext<CovidContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Api"));
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsChart",
                    builder => builder.WithOrigins("http://localhost:52048")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsChart");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CovidHub>("/CovidHub");
            });
        }
    }
}
