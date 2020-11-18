using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KashBookStore.Models.DataLayer;
using KashBookStore.Models.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KashBookStore
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
            services.AddRouting(options=> options.LowercaseUrls=true);

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<BookstoreContext>()
            .AddDefaultTokenProviders();

            services.AddMemoryCache();
            services.AddSession();
            services.AddDbContext<BookstoreContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BookStoreContext")));
            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
 
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                // Admin Area routing
                endpoints.MapAreaControllerRoute(
                    name:"admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Book}/{action=Index}/{id?}/"
                    );

                // paging, sorting, filtering
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}/filter/{author}/{genre}/{price}"
                    );

                // paging, and sorting
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller}/{action}/page/{pagenumber}/size/{pagesize}/sort/{sortfield}/{sortdirection}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });

            BookstoreContext.CreateAdminUser(app.ApplicationServices).Wait();
        }
    }
}
