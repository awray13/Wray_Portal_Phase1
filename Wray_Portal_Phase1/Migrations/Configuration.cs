namespace Wray_Portal_Phase1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;
    using Wray_Portal_Phase1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Wray_Portal_Phase1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Wray_Portal_Phase1.Models.ApplicationDbContext context)
        {

            #region Create Roles
            var roleManager = new RoleManager<IdentityRole>(
                                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Owner"))
            {
                roleManager.Create(new IdentityRole { Name = "Owner" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            if (!context.Roles.Any(r => r.Name == "NewUser"))
            {
                roleManager.Create(new IdentityRole { Name = "NewUser" });
            }
            #endregion

            #region Add Users and Assign Roles

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // Only Assigning the Role of Head of Household (Owner)
            if (!context.Users.Any(u => u.Email == "aricks1986@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "aricks1986@gmail.com",
                    Email = "aricks1986@gmail.com",
                    FirstName = "Ashton",
                    LastName = "Wray",
                    DisplayName = "Ashton",
                    EmailConfirmed = true,
                    AvatarPath = WebConfigurationManager.AppSettings["Default_Avatar"]
                };

                // This line creates the User and Users Password in the DB
                userManager.Create(user, "M@urice1");

                // This line attaches the Role of HOH to this specific user
                userManager.AddToRoles(user.Id, "Owner");

            }

            #endregion

            #region Seed Lookup Data
            context.BankAccountTypes.AddOrUpdate(
                b => b.Type,
                    new BankAccountType { Type = "Checking" },
                    new BankAccountType { Type = "Savings" },
                    new BankAccountType { Type = "Money Market" }

                );

            context.TransactionTypes.AddOrUpdate(
                t => t.Type,
                    new TransactionType { Type = "Deposit", Description = "Any transaction that results in money being added to your account"},
                    new TransactionType { Type = "Withdrawal", Description = "Any transaction that results in money being removed from your account" },
                    new TransactionType { Type = "Transfer", Description = "Any transaction that results in money being transferred from one account to another" }
                );
            #endregion

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
