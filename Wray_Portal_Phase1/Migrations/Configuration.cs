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

            #region Add Demo House
            context.Households.AddOrUpdate(
                h => h.Name,
                    new Household
                    {
                        Name = "Seeded House",
                        Created = DateTime.Now,
                        Greeting = "Welcome to the Seeded Demo House"
                    
                    });
            context.SaveChanges();

            #endregion

            #region Add Users and Assign Roles

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // Only Assigning the Role of Head of Household (Owner)
            var demoHouseId = context.Households.FirstOrDefault(h => h.Name == "Seeded House").Id;
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
                    AvatarPath = WebConfigurationManager.AppSettings["Default_Avatar"],
                    HouseholdId = demoHouseId
                    
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
            context.SaveChanges();

            #endregion
            #region Seed some BankAccounts

            var accountTypes = context.BankAccountTypes.ToList();
            var ownerId = context.Users.FirstOrDefault(u => u.Email == "aricks1986@gmail.com").Id;

            context.BankAccounts.AddOrUpdate(
                b => b.Name,
                    new BankAccount
                    {
                        Name = "Wells Fargo",
                        Created = DateTime.Now,
                        BankAccountTypeId = accountTypes.FirstOrDefault(a => a.Type == "Checking").Id,
                        HouseholdId = demoHouseId,
                        StartingBalance = 1000,
                        CurrentBalance = 1000,
                        LowBalanceLevel = 250,
                        OwnerId = ownerId
                    },
                    new BankAccount
                    {
                        Name = "Wells Fargo Savings",
                        Created = DateTime.Now,
                        BankAccountTypeId = accountTypes.FirstOrDefault(a => a.Type == "Savings").Id,
                        HouseholdId = demoHouseId,
                        StartingBalance = 3000,
                        CurrentBalance = 3000,
                        LowBalanceLevel = 250,
                        OwnerId = ownerId
                    });

            #endregion

            #region Seed some Categories
            context.Categories.AddOrUpdate(
                b =>b.Name,
                    new Category
                    {
                        Name = "Household Expenses",
                        TargetAmount = 1500,
                        Description = "This category will contain all the Household related recurring expenses including the mortgage",
                        HouseholdId = demoHouseId
                    },
                    new Category
                    {
                        Name = "Auto Expenses",
                        TargetAmount = 600,
                        Description = "This category will contain all the Auto related recurring expenses",
                        HouseholdId = demoHouseId
                    },
                    new Category
                    {
                        Name = "Entertainment Expenses",
                        TargetAmount = 300,
                        Description = "This category will contain all the Entertainment related recurring expenses",
                        HouseholdId = demoHouseId
                    });
            context.SaveChanges();

            #endregion

            #region Seed some Category Items
            var categories = context.Categories.ToList();

            context.CategoryItems.AddOrUpdate(
                    c => c.Name,
                        new CategoryItem
                        {
                            Name = "Mortgage",
                            Description = "Monthly mortgage for primary dwelling",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Household Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Gas Bill",
                            Description = "Monthly gas bill",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Household Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Electric Bill",
                            Description = "Monthly electric bill",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Household Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Water Bill",
                            Description = "Monthly water bill",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Household Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Car Payment",
                            Description = "Monthly car payment",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Auto Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Car Fuel",
                            Description = "Monthly car fuel",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Auto Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Insurance Payment",
                            Description = "Monthly insurance for vehicle",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Auto Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Dinner",
                            Description = "Night out with Hubby",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Entertainment Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Movies",
                            Description = "Treat outing",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Entertainment Expenses").Id

                        },
                        new CategoryItem
                        {
                            Name = "Laser Tag",
                            Description = "Treat outing",
                            CategoryId = categories.FirstOrDefault(c => c.Name == "Entertainment Expenses").Id

                        });

            #endregion

            context.TransactionTypes.AddOrUpdate(
                t => t.Type,
                    new TransactionType { Type = "Deposit", Description = "Any transaction that results in money being added to your account"},
                    new TransactionType { Type = "Withdrawal", Description = "Any transaction that results in money being removed from your account" },
                    new TransactionType { Type = "Transfer", Description = "Any transaction that results in money being transferred from one account to another" }
                );
            

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
