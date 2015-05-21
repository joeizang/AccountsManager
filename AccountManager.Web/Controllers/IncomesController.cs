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
    public class IncomesController : Controller
    {
        //private IRepository<Income> inc;
        //private IRepository<IncomeCategory> ic;
        //private IRepository<Payer> pa;
        //private IRepository<Receiver> rc;
        private IAccountManagerdb inc;

        public IncomesController(IAccountManagerdb db)
        {
            inc = db;
        }

        // GET: Incomes
        public async Task<ActionResult> Index()
        {
            var income = inc.Income.AsQueryable()
                                                  .Include(i => i.IncomeCategory)
                                                  .Include(i => i.Payer)
                                                  .Include(i => i.Receiver);
            return View(await income.ToListAsync());
        }

        // GET: Incomes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = await inc.Income.FirstOrDefaultAsync(x => x.IncomeId == id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // GET: Incomes/Create
        public ActionResult Create()
        {
            ViewBag.IncomeCategoryId = new SelectList(inc.IncomeCategory, "IncomeCategoryId", "Name");
            ViewBag.PayerId = new SelectList(inc.Payer, "PayerId", "Name");
            ViewBag.ReceiverId = new SelectList(inc.Receiver, "ReceiverId", "Name");
            return View();
        }

        // POST: Incomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IncomeId,PayerId,ReceiverId,Tithe,IncomeCategoryId,Amount,Description,Date")] Income income)
        {
            if (ModelState.IsValid)
            {
                inc.Income.Add(income);
                await inc.SaveChangesAsync(); // Log activity
                return RedirectToAction("Index");
            }

            ViewBag.IncomeCategoryId = new SelectList(inc.IncomeCategory, "IncomeCategoryId", "Name", income.IncomeCategoryId);
            ViewBag.PayerId = new SelectList(inc.Payer, "PayerId", "Name", income.PayerId);
            ViewBag.ReceiverId = new SelectList(inc.Receiver, "ReceiverId", "Name", income.ReceiverId);
            return View(income);
        }

        // GET: Incomes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = await inc.Income.FirstOrDefaultAsync(x => x.IncomeId == id);
            if (income == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncomeCategoryId = new SelectList(inc.IncomeCategory, "IncomeCategoryId", "Name", income.IncomeCategoryId);
            ViewBag.PayerId = new SelectList(inc.Payer, "PayerId", "Name", income.PayerId);
            ViewBag.ReceiverId = new SelectList(inc.Receiver, "ReceiverId", "Name", income.ReceiverId);
            return View(income);
        }

        // POST: Incomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IncomeId,PayerId,ReceiverId,Tithe,IncomeCategoryId,Amount,Description,Date")] Income income)
        {
            if (ModelState.IsValid)
            {
                inc.Entry(income).State = EntityState.Modified;
                await inc.SaveChangesAsync(); //remember to duplicate and log
                return RedirectToAction("Index");
            }
            ViewBag.IncomeCategoryId = new SelectList(inc.IncomeCategory, "IncomeCategoryId", "Name", income.IncomeCategoryId);
            ViewBag.PayerId = new SelectList(inc.Payer, "PayerId", "Name", income.PayerId);
            ViewBag.ReceiverId = new SelectList(inc.Receiver, "ReceiverId", "Name", income.ReceiverId);
            return View(income);
        }

        // GET: Incomes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Income income = await inc.Income.FirstOrDefaultAsync(x => x.IncomeId == id);
            if (income == null)
            {
                return HttpNotFound();
            }
            return View(income);
        }

        // POST: Incomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Income income = await inc.Income.FirstOrDefaultAsync(x => x.IncomeId == id);
            inc.Income.Remove(income);
            if (inc.Entry(income).State == EntityState.Deleted)
            {
                await inc.SaveChangesAsync(); //remember to log and mark as deleted (not visible to clients)
            }
            else
            {
                return View(income);
            }
                
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                inc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
