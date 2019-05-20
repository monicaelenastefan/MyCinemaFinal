using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;
using System.Web.Security;

namespace MyCinema.Controllers
{


    public class AccountController : Controller
    {


        // GET: Account
       MyModel db = new MyModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return RedirectToAction("Registration", "User");
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string username = form["username"].ToString();
            string password = form["password"].ToString();

            var usr = (from u in db.Users
                       where u.EmailID == username && u.Password == password && u.IsEmailVerified == true
                       select u).FirstOrDefault();

            if (usr != null)
            {
                FormsAuthentication.SetAuthCookie(usr.EmailID, false);
                if (usr.IsEmailVerified == false)
                {
                    TempData["Message"] = "Activate your account!";
                }
                return RedirectToAction("ForgotPassword", "Home");
            }
            TempData["Message"] = "Username and password are rong";
            return View();
        }


    }
}