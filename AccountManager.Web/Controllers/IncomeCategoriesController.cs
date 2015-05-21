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

namespace AccountManager.Web.Controllers
{
    public class IncomeCategoriesController : Controller
    {
        //private AccountManagerdb db = new AccountManagerdb();
        private IRepository<IncomeCategory> db;

        public IncomeCategoriesController(IRepository<IncomeCategory> repo)
        {
            db = repo;
        }

        // GET: IncomeCategories
        public async Task<ActionResult> Index()
        {
            var cat = db.GetAll().AsQueryable();
            return View(await cat.ToListAsync());
        }

        // GET: IncomeCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeCategory incomeCategory = await db.Query(x => x.IncomeCategoryId == id).FirstAsync();
            if (incomeCategory == null)
            {
                return HttpNotFound();
            }
            return View(incomeCategory);
        }

        // GET: IncomeCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IncomeCategoryId,Name,Description")] IncomeCategory incomeCategory)
        {
            if (ModelState.IsValid)
            {
                db.Add(incomeCategory);
                await db.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(incomeCategory);
        }

        // GET: IncomeCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeCategory incomeCategory = await db.Query(i => i.IncomeCategoryId == id).FirstAsync();
            if (incomeCategory == null)
            {
                return HttpNotFound();
            }
            return View(incomeCategory);
        }

        // POST: IncomeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IncomeCategoryId,Name,Description")] IncomeCategory incomeCategory)
        {
            if (ModelState.IsValid)
            {
                db.Update(incomeCategory);
                await db.CommitAsync(); // You want to make sure that Edits simply create a new duplicate document
                return RedirectToAction("Index");
            }
            return View(incomeCategory);
        }

        // GET: IncomeCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeCategory incomeCategory = await db.Query(x => x.IncomeCategoryId == id).FirstAsync();
            if (incomeCategory == null)
            {
                return HttpNotFound();
            }
            return View(incomeCategory);
        }

        // POST: IncomeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IncomeCategory incomeCategory = await db.Query(x => x.IncomeCategoryId == id).FirstAsync();
            db.Delete(incomeCategory);
            await db.CommitAsync(); // You will want to later ensure that deletes are just marked as delete but never deleted.
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
