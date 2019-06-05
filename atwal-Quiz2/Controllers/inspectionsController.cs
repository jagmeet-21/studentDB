using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using atwal_Quiz2.Models;

namespace atwal_Quiz2.Controllers
{
    public class inspectionsController : Controller
    {
        private Entities db = new Entities();

        // GET: inspections
        public ActionResult Index()
        {
            var inspections = db.inspections.Include(i => i.property).Include(i => i.staff);
            return View(inspections.ToList());
        }

        // GET: inspections/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inspection inspection = db.inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // GET: inspections/Create
        public ActionResult Create()
        {
            ViewBag.propNo = new SelectList(db.properties, "propNo", "propName");
            ViewBag.staffNo = new SelectList(db.staffs, "staffNo", "staffName");
            return View();
        }

        // POST: inspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "propNo,staffNo,date,time,comments")] inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.inspections.Add(inspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.propNo = new SelectList(db.properties, "propNo", "propName", inspection.propNo);
            ViewBag.staffNo = new SelectList(db.staffs, "staffNo", "staffName", inspection.staffNo);
            return View(inspection);
        }

        // GET: inspections/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inspection inspection = db.inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.propNo = new SelectList(db.properties, "propNo", "propName", inspection.propNo);
            ViewBag.staffNo = new SelectList(db.staffs, "staffNo", "staffName", inspection.staffNo);
            return View(inspection);
        }

        // POST: inspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "propNo,staffNo,date,time,comments")] inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.propNo = new SelectList(db.properties, "propNo", "propName", inspection.propNo);
            ViewBag.staffNo = new SelectList(db.staffs, "staffNo", "staffName", inspection.staffNo);
            return View(inspection);
        }

        // GET: inspections/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            inspection inspection = db.inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            inspection inspection = db.inspections.Find(id);
            db.inspections.Remove(inspection);
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
    }
}
