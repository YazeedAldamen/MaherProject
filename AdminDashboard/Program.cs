using DataLayer;
using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using System.ComponentModel;

namespace AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddMySqlCoreDb(builder.Configuration);
            builder.Services.AddTransient<UnitOfWorkServices>();
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
                options.EnableSensitiveDataLogging();
                options.ConfigureWarnings(t => t.Default(WarningBehavior.Log));
            });
            builder.Services.AddIdentity<AspNetUser,AspNetRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<MainDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();

            builder.Services.AddAuthentication();
            builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Auth/Login");
            builder.Services.AddAuthorization();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}