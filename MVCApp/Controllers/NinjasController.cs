using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace MVCApp.Controllers
{
    public class NinjasController : Controller
    {
        private NinjaContext db = new NinjaContext();

        // GET: Ninjas
        public ActionResult Index()
        {
            var ninjas = db.Ninjas.Include(n => n.Clan);
            return View(ninjas.ToList());
        }

        public ActionResult NinjaGrid()
        {
            var ninjas = db.Ninjas.Include(n => n.Clan);
            return View(ninjas.ToList());
        }

        public ActionResult ClanGrid()
        {
            var clans = db.Clans;
            return View(clans.ToList());
        }

        // GET: Ninjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        public ActionResult ClanDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        // GET: Ninjas/Create
        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View();
        }
        public ActionResult CreateClan()
        {
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View();
        }

        // POST: Ninjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                db.Ninjas.Add(ninja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View(ninja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClan([Bind(Include = "Id,ClanName,DateCreated,DateModified")] Clan clan)
        {
            if (ModelState.IsValid)
            {
                db.Clans.Add(clan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", clan);
            return View(clan);
        }

        // GET: Ninjas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);
    
            return View(ninja);
        }

        public ActionResult EditClan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", clan);

            return View(clan);
        }

        // POST: Ninjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ninja).State = EntityState.Modified;
                        db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // POST: Ninjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClan([Bind(Include = "Id,ClanName,DateCreated,DateModified")] Clan clan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", clan);
            return View(clan);
        }

        // GET: Ninjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        public ActionResult DeleteClan(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            return View(clan);
        }

        // POST: Ninjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ninja ninja = db.Ninjas.Find(id);
            db.Ninjas.Remove(ninja);
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

        // POST: Ninjas/Delete/5
        [HttpPost, ActionName("DeleteClan")]
        [ValidateAntiForgeryToken]
        public ActionResult ClanDeleteConfirmed(int id)
        {
            Clan clan = db.Clans.Find(id);
            db.Clans.Remove(clan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
