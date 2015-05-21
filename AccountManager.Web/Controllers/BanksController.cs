using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountManager.Domain.Contexts;
using AccountManager.Domain.Entities;
using AccountManager.Domain.Abstractions;

namespace AccountManager.Web.Controllers
{
    public class BanksController : Controller
    {
        private IAccountManagerdb db;

        public BanksController(IAccountManagerdb repo)
        {
            db = repo;
        }

        // GET: Banks
        public async Task<ActionResult> Index()
        {
            var bank = db.Bank.AsQueryable().Include(b => b.Accounts);
            return View(await bank.ToListAsync());
        }

        // GET: Banks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = await db.Bank.FirstOrDefaultAsync(x => x.BankId == id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BankId,BankName")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                db.Bank.Add(bank);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName");
            return View(bank);
        }

        // GET: Banks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = await db.Bank.FirstOrDefaultAsync(x => x.BankId == id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BankId,BankName")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bank).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName");
            return View(bank);
        }

        // GET: Banks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = await db.Bank.FirstOrDefaultAsync(x => x.BankId == id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bank bank = await db.Bank.FirstOrDefaultAsync(x => x.BankId == id);
            db.Bank.Remove(bank);
            await db.SaveChangesAsync();
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
