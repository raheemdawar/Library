using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Custom_Attributes;
using LibraryProject.DataBase;
namespace LibraryProject.Controllers
{
    //[AuthorizationFilter]
    public class HomeController : Controller
    {
        LiabraryEntities db = new LiabraryEntities();
        public ActionResult Index()
        {
  
            ViewBag.studentCount = db.Students.Count();
            ViewBag.bookCount = db.Books.Count();
            ViewBag.placeCount = db.Places.Count();
            ViewBag.issuedBookCount = db.IssuedBooks.Count();

            return View();
        }
        
            public ActionResult search()
        {
            return View();
        }


    }
}