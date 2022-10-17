using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using on_e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using on_e_commerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace on_e_commerce
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
            services.AddControllersWithViews();
            services.AddDbContext<dbEticaretContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("dbEticaretEntities")));

            services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<dbEticaretContext>()
    .AddDefaultTokenProviders()
    .AddRoles<AppRole>()
    .AddDefaultUI();

            services.AddMvc().AddRazorPagesOptions(options=> {
   options.Conventions.AddAreaPageRoute("AccountController", "/Account/Login",""); 
});
    
    

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                options.User.RequireUniqueEmail = true;
            });


            
            services.AddDistributedMemoryCache();
            services.AddRazorPages();


            services.AddMvc();
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "authToken";
        /// control cookie expiration
        options.Cookie.Expiration = TimeSpan.FromMinutes(120);
        options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
        options.Events = new CookieAuthenticationEvents()
    
        {
            OnRedirectToLogin = (context) =>
            {
                context.HttpContext.Response.Redirect("https://example.com/test/expired.html");
                return Task.CompletedTask;
            }
        };
    });
            services.AddControllers();

            

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

            app.UseSession();  
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

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
