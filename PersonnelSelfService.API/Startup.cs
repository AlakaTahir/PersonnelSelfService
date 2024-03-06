using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Personnel.Service.Interface;
using Personnel.Service.Service;
using PersonnelSelfService.Migrations;
using System;
using Microsoft.AspNetCore.Identity;
using Personnel.Model.Entity;
using Personnel.Service.Providers.JWT;
using AutoMapper;
using Personnel.Service.Mapping;
using PersonnelSelfService.API.Invocables;
using Coravel;
using Personnel.Service.Providers.Termii;
using PersonnelSelfService.Core;

namespace PersonnelSelfService.API
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

            services.AddControllers();
            
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAuthTokenProvider, AuthTokenProvider>();
            services.AddTransient<ILoanService, LoanService>();
			services.AddTransient<ISalaryAdvanceService, SalaryAdvanceService>();
			services.AddTransient<IUtility, Utility>();
			services.AddDbContext<PersonnelDatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddUnitOfWork<PersonnelDatabaseContext>();
            services.AddAutoMapper(typeof(AutoMapperProfile));

			services.AddHttpClient<ITermiiProvider, TermiiProvider>();

			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });

            services.AddTransient<HelloInvocable>();
            services.AddScheduler();

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler.Schedule<HelloInvocable>().EveryMinute();
            });
        }
    }
}
