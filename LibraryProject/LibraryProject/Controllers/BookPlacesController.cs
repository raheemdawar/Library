using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Custom_Attributes;
using LibraryProject.DataBase;

namespace LibraryProject.Controllers
{
    [AuthorizationFilter]
    public class BookPlacesController : Controller
    {
        private LiabraryEntities db = new LiabraryEntities();

        // GET: BookPlaces
        public ActionResult Index()
        {
            var bookPlaces = db.BookPlaces.Include(b => b.Book).Include(b => b.Place);
            return View(bookPlaces.ToList());
        }

        // GET: BookPlaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookPlace bookPlace = db.BookPlaces.Find(id);
            if (bookPlace == null)
            {
                return HttpNotFound();
            }
            return View(bookPlace);
        }

        // GET: BookPlaces/Create
        public ActionResult Create()
        {
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.FKPlaceID = new SelectList(db.Places, "PlaceID", "PlaceName");
            return View();
        }

        // POST: BookPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookPlaceID,FKBookID,FKPlaceID,isActive,ExtraLocationInfo")] BookPlace bookPlace)
        {
            if (ModelState.IsValid)
            {
                db.BookPlaces.Add(bookPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", bookPlace.FKBookID);
            ViewBag.FKPlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", bookPlace.FKPlaceID);
            return View(bookPlace);
        }

        // GET: BookPlaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookPlace bookPlace = db.BookPlaces.Find(id);
            if (bookPlace == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", bookPlace.FKBookID);
            ViewBag.FKPlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", bookPlace.FKPlaceID);
            return View(bookPlace);
        }

        // POST: BookPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookPlaceID,FKBookID,FKPlaceID,isActive,ExtraLocationInfo")] BookPlace bookPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", bookPlace.FKBookID);
            ViewBag.FKPlaceID = new SelectList(db.Places, "PlaceID", "PlaceName", bookPlace.FKPlaceID);
            return View(bookPlace);
        }

        // GET: BookPlaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookPlace bookPlace = db.BookPlaces.Find(id);
            if (bookPlace == null)
            {
                return HttpNotFound();
            }
            return View(bookPlace);
        }

        // POST: BookPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookPlace bookPlace = db.BookPlaces.Find(id);
            db.BookPlaces.Remove(bookPlace);
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
