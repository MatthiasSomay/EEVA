using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EEVA.Domain;
using Microsoft.EntityFrameworkCore;
using EEVA.Domain.Models;
using EEVA.Domain.Models.Interface;
using EEVA.Domain.DataManager;
using Microsoft.AspNetCore.Identity;
using EEVA.Web.Data;

namespace EEVA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string[] ROLES = new string[] { "Admin", "Teacher", "Student" };
        // Gebaseerd op https://dotnetdetail.net/role-based-authorization-in-asp-net-core-3-0/
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            //here in this line we are adding the Roles
            foreach (string role in ROLES)
            {
                var roleCheck = await RoleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    //here in this line we are creating admin role and seed it to the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
            IdentityUser user = await UserManager.FindByEmailAsync("jens.vanbever@student.ucll.be");
            

            await UserManager.AddToRoleAsync(user, "Student");
          

        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<EEVAContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:EEVA"]));
            services.AddDbContext<SecurityContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:SecurityContextConnection"]));
            services.AddScoped<IDataManager<Exam>, ExamManager>();
            services.AddControllersWithViews();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
        }
    }
}
