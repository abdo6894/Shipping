using DAL.Data.DbContext;
using Domains;
using Microsoft.AspNetCore.Identity;

namespace Ui.Services
{
    public class ContextConfig
    {
        private static readonly string SeedAdminEmail = "Abdullah@gmail.com";
        public static async Task SeedDataAsync(ShipingContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, roleManager);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Exists Role 
           if(!await roleManager.RoleExistsAsync("Admin"))
           {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
           }
           if (!await roleManager.RoleExistsAsync("User"))
           {
                await roleManager.CreateAsync(new IdentityRole("User"));
           }
            var AdminEmail = SeedAdminEmail;
            var AdminUser= await userManager.FindByEmailAsync(AdminEmail);
            if (AdminUser == null)
            {
                var id = Guid.NewGuid().ToString();
                AdminUser = new ApplicationUser
                {
                    Id = id,
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    FirstName="Abdullah",
                    LastName="Hashem",
                    Phone = "01063188954",

                };
                var resulet= await userManager.CreateAsync(AdminUser,"Abdullah304106@");
                await userManager.AddToRoleAsync(AdminUser, "Admin");


            }

        }
    }
}
