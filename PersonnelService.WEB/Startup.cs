using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Personnel.Model.Entity;
using Personnel.Service.Interface;
using Personnel.Service.Mapping;
using Personnel.Service.Providers.Termii;
using Personnel.Service.Service;
using PersonnelSelfService.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelService.WEB
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
			services.AddIdentity<AppIdentityUser, IdentityRole>(options =>
			{
				options.Password.RequiredLength = 4;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireDigit = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;

			}).AddEntityFrameworkStores<PersonnelDatabaseContext>().AddDefaultTokenProviders();

			services.AddControllersWithViews();

			services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILeaveService, LeaveService>();
            services.AddTransient<ILoanService, LoanService>();
			services.AddHttpClient<ITermiiProvider, TermiiProvider>();

			services.AddDbContext<PersonnelDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<PersonnelDatabaseContext>();
			services.AddAutoMapper(typeof(AutoMapperProfile));
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
