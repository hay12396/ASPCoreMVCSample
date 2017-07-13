using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CompaniesEx.Context;
using Microsoft.EntityFrameworkCore;
using CompaniesEx.Models.Repositories.Companies;
using CompaniesEx.Models.Repositories.Employees;
using AutoMapper;
using CompaniesEx.ViewModels;
using CompaniesEx.Models;

namespace CompaniesEx
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICompaniesRepository, CompaniesRepository>();
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Mapper.Initialize(config => {
                config.CreateMap<AddEditCompanyViewModel, Company>();
                config.CreateMap<Company, AddEditCompanyViewModel>();

                config.CreateMap<AddEditEmployeeViewModel, Employee>();
                config.CreateMap<Employee, AddEditEmployeeViewModel>();
            });

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Companies}/{action=Index}");
            });
        }
    }
}
