using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Leave_Management.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Leave_Management.Contracts;
using Leave_Management.Repository;
using AutoMapper;
using Leave_Management.Mappings;

namespace Leave_Management
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
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //Adding References for Repositories and Contracts
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();                   //Add each Repository here
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ILeaveHistoryRepository, LeaveHistoryRepository>();

            //Add Service for Automapper
            services.AddAutoMapper(typeof(Maps));     //In the brackets, put the class you created with your mappings

            //In the Options section below you can set password rules, lockout rules and many other things.  We have removed the requirement for e-mail confirmation of the account
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            services.AddDefaultIdentity<IdentityUser>()     //Trainer had to change this to Employee, but mine worked as is (module 31)
                                                            //If you do it here, you have to replace all instances of 'IdentityUser' in the project
                .AddRoles<IdentityRole>()                   //We added this to be able to 'seed' the database with default roles when it is run for the first time
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
                              UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)  //Added this for the seeding of the database, to add default roles and Admin user
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            SeedData.Seed(userManager, roleManager);    //Method to create default roles and Admin user - see class SeedData.cs

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
