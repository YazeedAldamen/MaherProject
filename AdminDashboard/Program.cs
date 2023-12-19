using DataLayer;
using DataLayer.DbContext;
using DataLayer.Entities;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceLayer;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Localization
            builder.Services.AddSingleton<LanguageService>();
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) => {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResource", assemblyName.Name);
                };
            });
            builder.Services.Configure<RequestLocalizationOptions>(options => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar")
                };
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });
            #endregion

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddMySqlCoreDb(builder.Configuration);
            builder.Services.AddTransient<ImageService>();
            builder.Services.AddTransient<UnitOfWorkServices>();
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")!);
                options.EnableSensitiveDataLogging();
                options.ConfigureWarnings(t => t.Default(WarningBehavior.Log));
            });

            //builder.Services.AddDefaultIdentity<DataLayer.Entities.AspNetUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MainDbContext>();
            builder.Services.AddIdentity<AspNetUser, AspNetRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

            }).AddEntityFrameworkStores<MainDbContext>()
              .AddDefaultTokenProviders()
              .AddSignInManager<SignInManager<AspNetUser>>()
              .AddRoles<AspNetRole>();

            builder.Services.AddAuthentication();
            builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Auth/Login");
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<EmailSender>();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.Configure<IISServerOptions>(options =>
            {
                // Set the maximum request size (100 MB in this example)
                options.MaxRequestBodySize = 104857600;
            });
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

            app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapFallbackToFile("/_Host.cshtml");
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}