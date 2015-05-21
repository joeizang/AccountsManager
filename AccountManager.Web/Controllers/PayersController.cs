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
    public class PayersController : Controller
    {
        private IRepository<Payer> db;

        public PayersController(IRepository<Payer> pr)
        {
            db = pr;
        }

        // GET: Payers
        public async Task<ActionResult> Index()
        {
            var pr = db.GetAll().AsQueryable();
            return View(await pr.ToListAsync());
        }

        // GET: Payers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = await db.Query(p => p.PayerId == id).FirstAsync();
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // GET: Payers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PayerId,Name")] Payer payer)
        {
            if (ModelState.IsValid)
            {
                db.Add(payer);
                await db.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(payer);
        }

        // GET: Payers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = await db.Query(p => p.PayerId == id).FirstAsync();
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // POST: Payers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PayerId,Name")] Payer payer)
        {
            if (ModelState.IsValid)
            {
                db.Update(payer);
                await db.CommitAsync(); // don't forget to not update without making a duplicate of the existing record first.
                return RedirectToAction("Index");
            }
            return View(payer);
        }

        // GET: Payers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payer payer = await db.Query(p => p.PayerId == id).FirstAsync();
            if (payer == null)
            {
                return HttpNotFound();
            }
            return View(payer);
        }

        // POST: Payers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payer payer = await db.Query(p => p.PayerId == id).FirstAsync();
            db.Delete(payer);
            await db.CommitAsync();
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
