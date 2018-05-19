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
    public class IssuedBooksController : Controller
    {
        private LiabraryEntities db = new LiabraryEntities();

        // GET: IssuedBooks
        public ActionResult Index()
        {
            var issuedBooks = db.IssuedBooks.Include(i => i.Book).Include(i => i.Student);
            return View(issuedBooks.ToList());
        }

        // GET: IssuedBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssuedBook issuedBook = db.IssuedBooks.Find(id);
            if (issuedBook == null)
            {
                return HttpNotFound();
            }
            return View(issuedBook);
        }

        // GET: IssuedBooks/Create
        public ActionResult Create()
        {
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName");
            ViewBag.FKStudentID = new SelectList(db.Students, "Student_ID", "StudentName");
            return View();
        }

        // POST: IssuedBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IssuedBookID,FKBookID,FKStudentID,createDate,isActive,Status")] IssuedBook issuedBook)
        {
            if (ModelState.IsValid)
            {
                db.IssuedBooks.Add(issuedBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", issuedBook.FKBookID);
            ViewBag.FKStudentID = new SelectList(db.Students, "Student_ID", "StudentName", issuedBook.FKStudentID);
            return View(issuedBook);
        }

        // GET: IssuedBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssuedBook issuedBook = db.IssuedBooks.Find(id);
            if (issuedBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", issuedBook.FKBookID);
            ViewBag.FKStudentID = new SelectList(db.Students, "Student_ID", "StudentName", issuedBook.FKStudentID);
            return View(issuedBook);
        }

        // POST: IssuedBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssuedBookID,FKBookID,FKStudentID,createDate,isActive,Status")] IssuedBook issuedBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issuedBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKBookID = new SelectList(db.Books, "BookID", "BookName", issuedBook.FKBookID);
            ViewBag.FKStudentID = new SelectList(db.Students, "Student_ID", "StudentName", issuedBook.FKStudentID);
            return View(issuedBook);
        }

        // GET: IssuedBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssuedBook issuedBook = db.IssuedBooks.Find(id);
            if (issuedBook == null)
            {
                return HttpNotFound();
            }
            return View(issuedBook);
        }

        // POST: IssuedBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IssuedBook issuedBook = db.IssuedBooks.Find(id);
            db.IssuedBooks.Remove(issuedBook);
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
