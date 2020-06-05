﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wray_Portal_Phase1.Helper;
using Wray_Portal_Phase1.Models;

namespace Wray_Portal_Phase1.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private NotificationHelper notificationHelper = new NotificationHelper();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.CategoryItem).Include(t => t.Owner).Include(t => t.TransactionType);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "Name");
            ViewBag.CategoryItemId = new SelectList(db.CategoryItems, "Id", "Name");
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionTypeId,BankAccountId,CategoryItemId,Amount,Memo")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Created = DateTime.Now;
                transaction.OwnerId = User.Identity.GetUserId();
                db.Transactions.Add(transaction);
                

                // Do I need to update or adjust the Associated Bank Account Current Balance... Yes
                // Reference to bank account
                var bank = db.BankAccounts.Find(transaction.BankAccountId);
                var transType = db.TransactionTypes.Find(transaction.TransactionTypeId).Type;
                if (transType == "Deposit")
                {
                    bank.CurrentBalance += transaction.Amount;
                }
                else 
                {
                    bank.CurrentBalance -= transaction.Amount;
                }
                db.SaveChanges();



                // Do I need to generate a Notification for either an overdraft Or a low balance breach...?
                notificationHelper.ManageNotifications(bank, transaction);


                return RedirectToAction("Dashboard", "Households");
            }

            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "OwnerId", transaction.BankAccountId);
            ViewBag.CategoryItemId = new SelectList(db.CategoryItems, "Id", "Name", transaction.CategoryItemId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", transaction.OwnerId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "OwnerId", transaction.BankAccountId);
            ViewBag.CategoryItemId = new SelectList(db.CategoryItems, "Id", "Name", transaction.CategoryItemId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", transaction.OwnerId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransactionTypeId,BankAccountId,CategoryItemId,OwnerId,Amount,Memo,Created")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "Id", "OwnerId", transaction.BankAccountId);
            ViewBag.CategoryItemId = new SelectList(db.CategoryItems, "Id", "Name", transaction.CategoryItemId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", transaction.OwnerId);
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "Id", "Type", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
