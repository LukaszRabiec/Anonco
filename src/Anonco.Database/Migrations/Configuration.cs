namespace Anonco.Database.Migrations
{
    using System;
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
            SeedModAccount(context);
            SeedCategories(context);
            SeedAnnouncements(context);
            SeedAnnouncementCategory(context);
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

            if (!roleManager.RoleExists(AppStrings.ModRoleName))
            {
                var role = new IdentityRole();
                role.Name = AppStrings.ModRoleName;

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

            if (!context.Users.Any(u => u.UserName == AppStrings.AdminUserName))
            {
                var admin = new User();
                admin.UserName = AppStrings.AdminUserName;
                admin.Email = AppStrings.AdminEmail;
                admin.FirstName = AppStrings.AdminFirstName;
                admin.LastName = AppStrings.AdminLastName;
                admin.EmailConfirmed = true;

                var adminResult = userManager.Create(admin, AppStrings.AdminPassword);

                if (adminResult.Succeeded)
                {
                    userManager.AddToRole(admin.Id, AppStrings.AdminRoleName);
                }
            }
        }

        private void SeedModAccount(ApplicationDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "mod"))
            {
                var admin = new User();
                admin.UserName = "mod";
                admin.Email = "mod@anonco.net";
                admin.FirstName = "Don";
                admin.LastName = "Pedro";
                admin.EmailConfirmed = true;

                var adminResult = userManager.Create(admin, AppStrings.AdminPassword);

                if (adminResult.Succeeded)
                {
                    userManager.AddToRole(admin.Id, AppStrings.ModRoleName);
                }
            }
        }

        private void SeedCategories(ApplicationDbContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                var category = new Category();
                category.Id = i;
                category.Name = $"Category {i}";
                category.ParentId = i;

                context.Set<Category>().AddOrUpdate(category);
            }

            context.SaveChanges();
        }

        private void SeedAnnouncements(ApplicationDbContext context)
        {
            var admin = context.Set<User>()
                .FirstOrDefault(u => u.UserName == AppStrings.AdminUserName);

            for (int i = 1; i <= 10; i++)
            {
                var announcement = new Announcement();
                announcement.Id = i;
                announcement.UserId = admin.Id;
                announcement.Title = $"Title {i}";
                announcement.Content = $"Content {i}";
                announcement.Email = admin.Email;
                announcement.PhoneNumber = "+48 123 456 789";
                announcement.AdditionDate = DateTimeOffset.Now.AddDays(-i);

                context.Set<Announcement>().AddOrUpdate(announcement);
            }

            context.SaveChanges();
        }

        private void SeedAnnouncementCategory(ApplicationDbContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                var annCat = new AnnouncementCategory();
                annCat.Id = i;
                annCat.AnnouncementId = i / 2 + 1;
                annCat.CategoryId = i / 2 + 2;

                context.Set<AnnouncementCategory>().AddOrUpdate(annCat);
            }

            context.SaveChanges();
        }
    }
}
