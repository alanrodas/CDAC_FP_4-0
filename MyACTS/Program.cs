using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyACTS.Data;
using MyACTS.Models.Entities;
using MyACTS.Services;
using static System.Formats.Asn1.AsnWriter;

namespace MyACTS;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        IAppConfig config = new AppConfig(connectionString) {
            LoginPath = "/Auth/Login",
            LogoutPath = "/Auth/Logout",
            ReturnUrlParameter = "/Home",
            ErrorPage = "/Home/Error",
            DefaultController = "Home",
            DefaultAction = "Index",
            RoleRedirects = new Dictionary<string, string>() {
                { "admin", "/Admin" },
                { "student", "/Dashboard" }
            }
        };

        // Add services to the container.

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        /*
        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        */

        builder.Services.AddControllersWithViews();
        

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options => {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                    options.ReturnUrlParameter = config.ReturnUrlParameter;
                    options.LoginPath = config.LoginPath;
                    options.LogoutPath = config.LogoutPath;
                }
            );

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSingleton(typeof(IAppConfig), config);

        builder.Services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() ) {
            app.UseMigrationsEndPoint();
        } else {
            app.UseExceptionHandler(config.ErrorPage);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCookiePolicy(new CookiePolicyOptions {
            MinimumSameSitePolicy = SameSiteMode.Strict,
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller="+config.DefaultController+"}/{action="+config.DefaultAction+"}/{id?}");

        // app.MapRazorPages();

        app.Run();
    }
}

