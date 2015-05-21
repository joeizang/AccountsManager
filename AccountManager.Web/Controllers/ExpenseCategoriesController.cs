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
    public class ExpenseCategoriesController : Controller
    {
        private IRepository<ExpenseCategory> db;

        public ExpenseCategoriesController(IRepository<ExpenseCategory> ec)
        {
            db = ec;
        }

        // GET: ExpenseCategories
        public async Task<ActionResult> Index()
        {
            var ec = db.GetAll().AsQueryable();
            return View(await ec.ToListAsync());
        }

        // GET: ExpenseCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = await db.Query(e => e.ExpenseCategoryId == id).FirstAsync();
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExpenseCategoryId,Name,Description")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Add(expenseCategory);
                await db.CommitAsync(); // log every entry made across the entire application.
                return RedirectToAction("Index");
            }

            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = await db.Query(e => e.ExpenseCategoryId == id).FirstAsync();
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExpenseCategoryId,Name,Description")] ExpenseCategory expenseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Update(expenseCategory);
                await db.CommitAsync(); // don't forget to make a duplicate of the record before updating and logging as well
                return RedirectToAction("Index");
            }
            return View(expenseCategory);
        }

        // GET: ExpenseCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseCategory expenseCategory = await db.Query(e => e.ExpenseCategoryId == id).FirstAsync(); 
            if (expenseCategory == null)
            {
                return HttpNotFound();
            }
            return View(expenseCategory);
        }

        // POST: ExpenseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ExpenseCategory expenseCategory = await db.Query(e => e.ExpenseCategoryId == id).FirstAsync();
            db.Delete(expenseCategory);
            await db.CommitAsync(); // Don't forget to just make a duplicate of the actual record and just mark as not visible.
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
