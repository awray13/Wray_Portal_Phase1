using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wray_Portal_Phase1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarPath { get; set; }

        public int? HouseholdId { get; set; }


        //Navigation Props
        public virtual Household Household { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public ApplicationUser()
        {
            Transactions = new HashSet<Transaction>();
        }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Wray_Portal_Phase1.Models.BankAccount> BankAccounts { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.BankAccountType> BankAccountTypes { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.Household> Households { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.Category> Categories { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.CategoryItem> CategoryItems { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.Transaction> Transactions { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.TransactionType> TransactionTypes { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.Invitation> Invitations { get; set; }

        public DbSet<Wray_Portal_Phase1.Models.Notification> Notifications { get; set; }
    }
}