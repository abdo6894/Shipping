using BL.Mapping;
using DAL.Data.DbContext;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BL.Services.Interfaces.Generic;
using Ui.Services;
using Microsoft.OpenApi.Writers;
using Domains;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace Ui
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            RegisterationServiceHelper.RegisterationService(builder);   


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

            app.UseAuthentication(); // „Â„ Ãœ« ·Ê ⁄‰œﬂ Identity
            app.UseAuthorization();

            // √Ê·«: Œ—Ìÿ… «·‹ Areas
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            // »⁄œÌ‰: «·„”«— «·«› —«÷Ì «·⁄«„
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope= app.Services.CreateScope())
            {
                var services= scope.ServiceProvider;
                var usermaneger = services.GetRequiredService<UserManager<ApplicationUser>>();
                var rolemaneger = services.GetRequiredService<RoleManager<IdentityRole>>();
                var dbcontext=services.GetRequiredService<ShippingContext>();
                 
                // Apply migration
                await dbcontext.Database.MigrateAsync();

                // seed data
                await ContextConfig.SeedDataAsync(dbcontext,usermaneger,rolemaneger);


            }
            app.Run();
        }
    }
}
