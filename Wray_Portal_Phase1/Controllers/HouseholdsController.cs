using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Wray_Portal_Phase1.Extensions;
using Wray_Portal_Phase1.Helper;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Controllers
{
    [Authorize]
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRoleHelper roleHelper = new UserRoleHelper();

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }


        public ActionResult Dashboard()
        {
            var houseId = db.Users.Find(User.Identity.GetUserId()).HouseholdId;
            var house = db.Households.Include(h => h.BankAccounts).Include(h => h.Notifications).FirstOrDefault(h => h.Id == houseId);

            // Setup the necessary data for the wizard
            ViewBag.AccountType = new SelectList(db.BankAccountTypes, "Id", "Type");

            return View(house);
        }


        public ActionResult Join()
        {
            return View();
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(string houseName, string greeting, string bankName, int accountType, decimal startingBalance, decimal warningBalance, string category, decimal target, string description, string categoryItem)
        {
            var newHouse = new Household 
            { 
                Name = houseName, 
                Greeting = greeting 
            };

            db.Households.Add(newHouse);
            newHouse.Created = DateTime.Now;
            var user = db.Users.Find(User.Identity.GetUserId());
            user.HouseholdId = newHouse.Id;
            db.SaveChanges();

            // Assign this user the role of Owner 
            roleHelper.AddUserToRole(user.Id, "Owner");

            var newBank = new BankAccount
            {
                HouseholdId = newHouse.Id,
                OwnerId = User.Identity.GetUserId(),
                Created = DateTime.Now,
                Name = bankName,
                BankAccountTypeId = accountType,
                StartingBalance = startingBalance,
                CurrentBalance = startingBalance,
                LowBalanceLevel = warningBalance
            };
            db.BankAccounts.Add(newBank);
            db.SaveChanges();

            var newCategory = new Category
            {
                HouseholdId = newHouse.Id, 
                Name = category,
                Description = description,
                TargetAmount = target
            };
            db.Categories.Add(newCategory);
            db.SaveChanges();

            var newItem = new CategoryItem
            {
                CategoryId = newCategory.Id,
                Description = description,
                Name = categoryItem
            };
            db.CategoryItems.Add(newItem);
            db.SaveChanges();

            // call the programmatic reauthorize extention method
            await HttpContextBaseExtension.RefreshAuthentication(HttpContext, user);


            return RedirectToAction("Dashboard");
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Name,Greeting")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult _SideNav()
        {
            var model = db.Users.Find(User.Identity.GetUserId());
            return PartialView(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
