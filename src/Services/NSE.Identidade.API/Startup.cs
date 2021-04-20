using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NSE.Identidade.API.Data;
using System;

namespace NSE.Identidade.API
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>() //support of role
                    .AddEntityFrameworkStores<ApplicationDbContext>() //source of data
                    .AddDefaultTokenProviders(); //add token to reset password or something like that

            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise Identity API",
                    Description = "This API is part of course ASP.NET Core Enterprise Applications.",
                    Contact = new OpenApiContact() { Name = "Pedro Fiuza", Email = "pdrfiuza@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(option => 
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
