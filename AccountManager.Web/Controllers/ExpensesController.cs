using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountManager.Domain.Entities;
using AccountManager.Domain.Contexts;
using AccountManager.Domain.Abstractions;

namespace AccountManager.Web.Controllers
{
    public class ExpensesController : Controller
    {
        //private IRepository<Expense> db;
        //private IRepository<ExpenseCategory> ec;
        //private IRepository<Payer> pa;
        //private IRepository<Receiver> rc;
        private IAccountManagerdb db;

        public ExpensesController(IAccountManagerdb db3)
        {
            db = db3;
        }

        // GET: Expenses
        public async Task<ActionResult> Index()
        {
            var expense = db.Expense.AsQueryable()
                                     .Include(e => e.ExpenseCategory)
                                     .Include(e => e.Payer)
                                     .Include(e => e.Receiver);
            return View(await expense.ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expense.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategory, "ExpenseCategoryId", "Name");
            ViewBag.PayerId = new SelectList(db.Payer, "PayerId", "Name");
            ViewBag.ReceiverId = new SelectList(db.Receiver, "ReceiverId", "Name");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseId,PayerId,ExpenseCategoryId,ReceiverId,Description,Date,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Expense.Add(expense);
                await db.SaveChangesAsync(); //Don't forget to log this activitiy
                return RedirectToAction("Index");
            }

            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategory, "ExpenseCategoryId", "Name", expense.ExpenseCategoryId);
            ViewBag.PayerId = new SelectList(db.Payer, "PayerId", "Name", expense.PayerId);
            ViewBag.ReceiverId = new SelectList(db.Receiver, "ReceiverId", "Name", expense.ReceiverId);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expense.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategory, "ExpenseCategoryId", "Name", expense.ExpenseCategoryId);
            ViewBag.PayerId = new SelectList(db.Payer, "PayerId", "Name", expense.PayerId);
            ViewBag.ReceiverId = new SelectList(db.Receiver, "ReceiverId", "Name", expense.ReceiverId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseId,PayerId,ExpenseCategoryId,ReceiverId,Description,Date,Amount")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                await db.SaveChangesAsync(); //Remember to log and back up record before updating
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategory, "ExpenseCategoryId", "Name", expense.ExpenseCategoryId);
            ViewBag.PayerId = new SelectList(db.Payer, "PayerId", "Name", expense.PayerId);
            ViewBag.ReceiverId = new SelectList(db.Receiver, "ReceiverId", "Name", expense.ReceiverId);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = await db.Expense.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Expense expense = await db.Expense.FirstOrDefaultAsync(x => x.ExpenseId == id);
            db.Expense.Remove(expense);
            await db.SaveChangesAsync(); // rememeber to mark as deleted and log
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
