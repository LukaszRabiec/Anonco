namespace Anonco.Database.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shared;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            SeedRoles(context);
            SeedAdminAccount(context);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists(AppStrings.AdminRoleName))
            {
                var role = new IdentityRole();
                role.Name = AppStrings.AdminRoleName;

                roleManager.Create(role);
            }
        }

        // I know it's not the best solution. It's temporary.
        // If you get here and you may have some ideas for creating
        // owner account from app without registration, send me message :)
        private void SeedAdminAccount(ApplicationDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == AppStrings.AdminRoleName))
            {
                var admin = new User();
                admin.UserName = "admin@anonco.net";
                admin.Email = "admin@anonco.net";       //it's fake (I hope)
                admin.FirstName = "Baltazar";
                admin.LastName = "G¹bka";

                var adminResult = userManager.Create(admin, "Zxc!23123");

                if (adminResult.Succeeded)
                {
                    userManager.AddToRole(admin.Id, AppStrings.AdminRoleName);
                }
            }
        }
    }
}
