using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestHouse.Application.Infastructure.Repositories;
using TestHouse.Application.Services;
using TestHouse.Infrastructure.Identity;
using TestHouse.Infrastructure.Repositories;

namespace TestHouse.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<ProjectRespository>
                (options => options.UseSqlServer(Configuration.GetConnectionString("TestHouseConnection")));
            services.AddDbContext <AppIdentityDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("TestHouseConnection")));
            services.AddScoped<IProjectRepository,ProjectRespository>();
            services.AddScoped<ProjectService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
