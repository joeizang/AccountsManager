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
    public class ReceiversController : Controller
    {
        private IRepository<Receiver> db;

        public ReceiversController(IRepository<Receiver> rc)
        {
            db = rc;
        }

        // GET: Receivers
        public async Task<ActionResult> Index()
        {
            var dir = db.GetAll().AsQueryable();
            return View(await dir.ToListAsync());
        }

        // GET: Receivers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = await db.Query(x => x.ReceiverId == id).FirstAsync();
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // GET: Receivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Receivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReceiverId,Name,Address")] Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                db.Add(receiver);
                await db.CommitAsync();
                return RedirectToAction("Index");
            }

            return View(receiver);
        }

        // GET: Receivers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = await db.Query(x => x.ReceiverId == id).FirstAsync();
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ReceiverId,Name,Address")] Receiver receiver)
        {
            if (ModelState.IsValid)
            {
                db.Update(receiver);
                await db.CommitAsync(); // remember to make a copy of the record first before any changes can happen.
                return RedirectToAction("Index");
            }
            return View(receiver);
        }

        // GET: Receivers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receiver receiver = await db.Query(x => x.ReceiverId == id).FirstAsync();
            if (receiver == null)
            {
                return HttpNotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Receiver receiver = await db.Query(x => x.ReceiverId == id).FirstAsync();
            db.Delete(receiver); //remember to  not delete the actual record
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
