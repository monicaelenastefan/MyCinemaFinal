using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;

namespace MyCinema.Controllers
{
    public class RoomPicsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: RoomPics
        public ActionResult Index()
        {
            
            return View(db.RoomPics.ToList());
        }

        // GET: RoomPics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPics roomPics = db.RoomPics.Find(id);
            if (roomPics == null)
            {
                return HttpNotFound();
            }
            return View(roomPics);
        }

        // GET: RoomPics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomPics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,RoomId,ImageUrl")] RoomPics roomPics)
        {
            if (ModelState.IsValid)
            {
                db.RoomPics.Add(roomPics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomPics);
        }

        // GET: RoomPics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPics roomPics = db.RoomPics.Find(id);
            if (roomPics == null)
            {
                return HttpNotFound();
            }
            return View(roomPics);
        }

        // POST: RoomPics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,RoomId,ImageUrl")] RoomPics roomPics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomPics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomPics);
        }

        // GET: RoomPics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomPics roomPics = db.RoomPics.Find(id);
            if (roomPics == null)
            {
                return HttpNotFound();
            }
            return View(roomPics);
        }

        // POST: RoomPics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomPics roomPics = db.RoomPics.Find(id);
            db.RoomPics.Remove(roomPics);
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
