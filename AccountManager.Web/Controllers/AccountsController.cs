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
using AccountManager.Domain.Repository;
using AccountManager.Domain.Abstractions;

namespace AccountManager.Web.Controllers
{
    public class AccountsController : Controller
    {
        //private IRepository<Accounts> db;
        //private IRepository<Bank> bank;
        private IAccountManagerdb db;

        public AccountsController(IAccountManagerdb _db)
        {
            db = _db;
        }

        // GET: Accounts
        public async Task<ActionResult> Index()
        {
            //var accounts = db.GetAll().AsQueryable().Include(a => a.Bank);
            var accounts = db.Accounts.AsQueryable().Include(a => a.Bank);
            return View(await accounts.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = await db.Accounts.FirstAsync(x => x.AccountsId == id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AccountsId,AccountName,AccountTypes,AccountBalance,BankId")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(accounts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName", accounts.BankId);
            return View(accounts);
        }

        // GET: Accounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Accounts accounts = await db.Query(x => x.AccountsId == id).FirstOrDefaultAsync();
            //var accounts = db.GetAll().AsQueryable().Any(x => x.AccountsId == id);
            var accounts = await db.Accounts.FirstAsync(x => x.AccountsId == id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName", accounts.BankId);
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AccountsId,AccountName,AccountTypes,AccountBalance,BankId")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.Bank, "BankId", "BankName", accounts.BankId);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = await db.Accounts.FirstAsync(x => x.AccountsId == id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Accounts accounts = await db.Accounts.FirstOrDefaultAsync(x => x.AccountsId == id);
            db.Accounts.Remove(accounts);
            await db.SaveChangesAsync(); // can implement my own override of savechanges where it doesn't actually delete but hides it
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
