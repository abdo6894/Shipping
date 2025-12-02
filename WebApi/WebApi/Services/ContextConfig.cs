using DAL.Data.DbContext;
using Domains;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Services
{
    public class ContextConfig
    {
        private static readonly string SeedAdminEmail = "Abdullah@gmail.com";
        private static readonly string SeedReviwerEmail = "Reviwer@gmail.com";
        private static readonly string SeedOperationEmail = "Operation@gmail.com";
        private static readonly string SeedOperationMangerEmail = "OperationManger@gmail.com";
        public static async Task SeedDataAsync(ShipingContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedUserAsync(userManager, roleManager);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Exists Role 
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Reviwer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Reviwer"));
            }

            if (!await roleManager.RoleExistsAsync("Operation"))
            {
                await roleManager.CreateAsync(new IdentityRole("Operation"));
            }

            if (!await roleManager.RoleExistsAsync("OperationManger"))
            {
                await roleManager.CreateAsync(new IdentityRole("OperationManger"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Admin
            var AdminEmail = SeedAdminEmail;
            var AdminUser = await userManager.FindByEmailAsync(AdminEmail);
            if (AdminUser == null)
            {
                var id = Guid.NewGuid().ToString();
                AdminUser = new ApplicationUser
                {
                    Id = id,
                    UserName = AdminEmail,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    FirstName = "Abdullah",
                    LastName = "Hashem",
                    Phone = "01063188954",
                };
                var result = await userManager.CreateAsync(AdminUser, "Abdullah304106@");
                await userManager.AddToRoleAsync(AdminUser, "Admin");
            }

            // Reviwer
            var ReviwerEmail = SeedReviwerEmail;
            var ReviwerUser = await userManager.FindByEmailAsync(ReviwerEmail);
            if (ReviwerUser == null)
            {
                var id = Guid.NewGuid().ToString();
                ReviwerUser = new ApplicationUser
                {
                    Id = id,
                    UserName = ReviwerEmail,
                    Email = ReviwerEmail,
                    EmailConfirmed = true,
                    FirstName = "Reviwer",
                    LastName = "User",
                    Phone = "01000000001",
                };
                var result = await userManager.CreateAsync(ReviwerUser, "Reviwer304106@");
                await userManager.AddToRoleAsync(ReviwerUser, "Reviwer");
            }

            // Operation
            var OperationEmail = SeedOperationEmail;
            var OperationUser = await userManager.FindByEmailAsync(OperationEmail);
            if (OperationUser == null)
            {
                var id = Guid.NewGuid().ToString();
                OperationUser = new ApplicationUser
                {
                    Id = id,
                    UserName = OperationEmail,
                    Email = OperationEmail,
                    EmailConfirmed = true,
                    FirstName = "Operation",
                    LastName = "User",
                    Phone = "01000000002",
                };
                var result = await userManager.CreateAsync(OperationUser, "Operation304106@");
                await userManager.AddToRoleAsync(OperationUser, "Operation");
            }

            // OperationManger
            var OperationMangerEmail = SeedOperationMangerEmail;
            var OperationMangerUser = await userManager.FindByEmailAsync(OperationMangerEmail);
            if (OperationMangerUser == null)
            {
                var id = Guid.NewGuid().ToString();
                OperationMangerUser = new ApplicationUser
                {
                    Id = id,
                    UserName = OperationMangerEmail,
                    Email = OperationMangerEmail,
                    EmailConfirmed = true,
                    FirstName = "Operation",
                    LastName = "Manager",
                    Phone = "01000000003",
                };
                var result = await userManager.CreateAsync(OperationMangerUser, "OperationManger304106@");
                await userManager.AddToRoleAsync(OperationMangerUser, "OperationManger");
            }
         
        }

    }
}


