using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Controllers
{
    [Authorize(Roles = "Owner, Member, NewUser")]
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            var houseId = db.Users.Find(User.Identity.GetUserId()).HouseholdId;
            var bankAccounts = db.BankAccounts.Where(b => b.HouseholdId == houseId).Include(b => b.BankAccountType).Include(b => b.Household).ToList();
            

            return View(bankAccounts);
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // GET: BankAccounts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BankAccountTypeId = new SelectList(db.BankAccountTypes, "Id", "Type");
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,BankAccountTypeId,Name,StartingBalance,CurrentBalance,LowBalanceLevel")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var houseId = db.Users.Find(userId).HouseholdId;
                bankAccount.OwnerId = userId;
                bankAccount.Created = DateTime.Now;
                bankAccount.HouseholdId = (int)houseId;
                db.BankAccounts.Add(bankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountTypeId = new SelectList(db.BankAccountTypes, "Id", "Type", bankAccount.BankAccountTypeId);
            
            return RedirectToAction("Index");
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountTypeId = new SelectList(db.BankAccountTypes, "Id", "Id", bankAccount.BankAccountTypeId);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccount.HouseholdId);
            return View(bankAccount);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,BankAccountTypeId,OwnerId,Created,Name,StartingBalance,CurrentBalance,LowBalanceLevel")] BankAccount bankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountTypeId = new SelectList(db.BankAccountTypes, "Id", "Id", bankAccount.BankAccountTypeId);
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccount.HouseholdId);
            return View(bankAccount);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccount bankAccount = db.BankAccounts.Find(id);
            if (bankAccount == null)
            {
                return HttpNotFound();
            }
            return View(bankAccount);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccount bankAccount = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(bankAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
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
