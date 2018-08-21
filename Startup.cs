using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CoreApi.Models;
using Microsoft.AspNetCore.Http;


namespace CoreApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin() 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
            var connection = @"Server=CSCVIEAI568403\SQLEXPRESS;Database=CoreApi;Trusted_Connection=true;MultipleActiveResultSets=true";
            
            services.AddDbContext<userContext>(options => options.UseSqlServer(connection));

            
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            app.UseMvc();
            
        }
    }
}