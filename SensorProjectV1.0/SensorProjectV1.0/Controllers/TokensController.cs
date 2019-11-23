using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SensorProjectV1._0.Models;

namespace SensorProjectV1._0.Controllers
{
    public class TokensController : Controller
    {
        private SensorDataBaseEntities db = new SensorDataBaseEntities();

        // GET: Tokens
        public ActionResult Index()
        {
            var token = db.Token.Include(t => t.Device);
            return View(token.ToList());
        }

        // GET: Tokens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Token.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            return View(token);
        }

        // GET: Tokens/Create
        public ActionResult Create()
        {
            ViewBag.DeviceID = new SelectList(db.Device, "DeviceID", "Type");
            return View();
        }

        // POST: Tokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TokenID,DeviceID,Timestamp")] Token token)
        {
            if (ModelState.IsValid)
            {
                db.Token.Add(token);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceID = new SelectList(db.Device, "DeviceID", "Type", token.DeviceID);
            return View(token);
        }

        // GET: Tokens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Token.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceID = new SelectList(db.Device, "DeviceID", "Type", token.DeviceID);
            return View(token);
        }

        // POST: Tokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TokenID,DeviceID,Timestamp")] Token token)
        {
            if (ModelState.IsValid)
            {
                db.Entry(token).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceID = new SelectList(db.Device, "DeviceID", "Type", token.DeviceID);
            return View(token);
        }

        // GET: Tokens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Token token = db.Token.Find(id);
            if (token == null)
            {
                return HttpNotFound();
            }
            return View(token);
        }

        // POST: Tokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Token token = db.Token.Find(id);
            db.Token.Remove(token);
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
