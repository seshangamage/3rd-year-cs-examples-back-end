using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace StudentMicroservice
{
    public class Startup
    {
        private const string FrontendCorsPolicy = "FrontendCorsPolicy";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StudentContext>(opt => opt.UseInMemoryDatabase("StudentDb"));
            services.AddCors(options =>
            {
                options.AddPolicy(FrontendCorsPolicy, builder =>
                {
                    builder
                        .WithOrigins("https://purple-tree-0684b981e.6.azurestaticapps.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseCors(FrontendCorsPolicy);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
