using DAL.Data.DbContext;
using Domains;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Ui.Services;
namespace Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            builder.Services.AddControllersWithViews();

            RegisterationServiceHelper.RegisterationService(builder); ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseRouting();

            app.UseAuthentication(); // مهم جدًا لو عندك Identity
            app.UseAuthorization();

            // أولًا: خريطة الـ Areas
            app.MapControllerRoute(
                  name: "admin",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            using (var scope= app.Services.CreateScope())
            {
                var services= scope.ServiceProvider;
                var usermaneger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var rolemaneger = services.GetRequiredService<RoleManager<IdentityRole>>();
                var dbcontext=services.GetRequiredService<ShipingContext>();
                 
                // Apply migration
                await dbcontext.Database.MigrateAsync();

                // seed data
                await ContextConfig.SeedDataAsync(dbcontext,usermaneger,rolemaneger);


            }
            app.Run();
        }
    }
}
