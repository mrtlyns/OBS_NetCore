using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OBS_Net.DAL.ORM.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OBS_Net.Entities.Tables;
using OBS_Net.BL.StudentManager;

namespace OBS_Net
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

            services.AddControllers();
            //services.AddControllersWithViews();//sonradan ekledim
            services.AddRazorPages();
            services.AddDbContext<ObsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ObsDB")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region 


            //DAL
            services.AddScoped(typeof(IObsNetRepository<>), typeof(ObsNetRepository<>));
            //BL
            services.AddScoped<IStudentManager, StudentManager>();
            //    services.AddScoped<ITeacherManager, TeacherManager>();
            //    services.AddScoped<ILessonManager, LessonManager>();
            //   services.AddScoped<ILessonForStudentManager, LessonForStudentManager>();
            #endregion
            #region //Dependency Injection

            //DAL
            services.AddScoped(typeof(IObsNetRepository<>), typeof(ObsNetRepository<>));
            //BL
            //services.AddScoped<IStudentManager, StudentManager>();

            #endregion
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
