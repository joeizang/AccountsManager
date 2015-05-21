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

namespace AccountManager.Web.Controllers
{
    public class TithesController : Controller
    {
        private IRepository<Tithe> db;
        private IRepository<Income> inc;

        public TithesController(IRepository<Tithe> the, IRepository<Income> db1)
        {
            db = the;
            inc = db1;
        }

        // GET: Tithes
        public async Task<ActionResult> Index()
        {
            var tithe = db.GetAll().AsQueryable().Include(t => t.Income);
            return View(await tithe.ToListAsync());
        }

        // GET: Tithes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = await db.Query(t => t.TitheId == id).FirstOrDefaultAsync();
            if (tithe == null)
            {
                return HttpNotFound();
            }
            return View(tithe);
        }

        // GET: Tithes/Create
        public ActionResult Create()
        {
            ViewBag.IncomeId = new SelectList(inc.GetAll(), "IncomeId", "Description");
            return View();
        }

        // POST: Tithes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TitheId,Amount,IncomeId,Date")] Tithe tithe)
        {
            if (ModelState.IsValid)
            {
                db.Add(tithe);
                await db.CommitAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IncomeId = new SelectList(inc.GetAll(), "IncomeId", "Description", tithe.IncomeId);
            return View(tithe);
        }

        // GET: Tithes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = await db.Query(t => t.TitheId == id).FirstOrDefaultAsync();
            if (tithe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncomeId = new SelectList(inc.GetAll(), "IncomeId", "Description", tithe.IncomeId);
            return View(tithe);
        }

        // POST: Tithes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TitheId,Amount,IncomeId,Date")] Tithe tithe)
        {
            if (ModelState.IsValid)
            {
                db.Update(tithe);
                await db.CommitAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IncomeId = new SelectList(inc.GetAll(), "IncomeId", "Description", tithe.IncomeId);
            return View(tithe);
        }

        // GET: Tithes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tithe tithe = await db.Query(t => t.TitheId == id).FirstOrDefaultAsync();
            if (tithe == null)
            {
                return HttpNotFound();
            }
            return View(tithe);
        }

        // POST: Tithes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tithe tithe = await db.Query(t => t.TitheId == id).FirstOrDefaultAsync();
            db.Delete(tithe);
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
