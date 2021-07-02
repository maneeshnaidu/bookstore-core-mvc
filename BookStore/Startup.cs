using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Repository;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using BookStore.Helpers;
using BookStore.Services;

namespace BookStore
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
            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<BookStoreContext>(
                options => options.UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString)));

            //Method to configure the Identity service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();

            //Configure password constraints
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

                //Configure Login constraints
                options.SignIn.RequireConfirmedEmail = true;
            });

            //Configure Login page redirection
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = Configuration["Application:LoginPath"];
            });


            services.AddControllersWithViews();

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
             
                //Uncomment to disable client side validations in debug mode
            //    .AddViewOptions(options =>
            //{
            //    options.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddSingleton<IMessageRepository, MessageRepository>();

            //Configure User Claims
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            //Configure custom UserService
            services.AddScoped<IUserService, UserService>();

            //Configure custom EmailService
            services.AddScoped<IEmailService, EmailService>();

            //Configure Email Service
            services.Configure<SMTPConfigModel>(Configuration.GetSection("SMTPConfig"));

            //Configure service to read configuration using IOptions
            services.Configure<NewBookAlertConfig>("InternalBook", Configuration.GetSection("NewBookAlertConfig"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook", Configuration.GetSection("ThirdPartyBook"));
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

            //Enable Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Default route
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();

                //Conventional Routing
                //endpoints.MapControllerRoute(
                //    name: "Privacy",
                //    pattern: "privacy",
                //    defaults: new { controller = "Home", action = "Privacy"});

                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
