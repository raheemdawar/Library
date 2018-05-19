using System.Web.Mvc;
using LibraryProject.Models;
using LibraryProject.DataAccessLayer;
using LibraryProject.DataBase;
using System.Web.Security;

namespace LibraryProject.Controllers
{
    public class SiteController : Controller
    {

        DAL _dataAccessLayer = new DAL();
        // GET: Site
        public ActionResult login()
        {
            return View();
        }
        public ActionResult verfiyAdmin(LoginModel model)
        {
           if(model!=null)
            {
                Admin data = _dataAccessLayer.getAdminDetailByEmail(model.email);
                if(data!=null)
                {

                    Session["adminName"] = data.adminName;
                    return RedirectToAction("index", "Home");

                }

            }

            return RedirectToAction("login","Site");
        }
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Site");
            ;
        }
    }
}