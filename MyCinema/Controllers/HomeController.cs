using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;
using System.Data.Entity;


namespace MyCinema.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string name)
        {
            //Pune in ViewBag Email-ul utilizatorului conectat
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;

            MyModel db = new MyModel();

            //Fac cate un ViewBag pentru firstname, lastname, username ( ca sa le folosesc in view )
            var usr = (from u in db.Users
                       where u.EmailID == nume
                       select u).FirstOrDefault();

            if (usr != null)
            {
                ViewBag.FirstName = usr.FirstName;
                ViewBag.LastName = usr.LastName;
                ViewBag.UserName = usr.Username;

            }


            return View();
        }

        // GET: Home
        public ActionResult Admin(string name)
        {
            MyModel db = new MyModel();

            ViewBag.TotalUsers = db.Users.Count();
            ViewBag.TotalMovies = db.Movies.Count();
            ViewBag.TotalRooms = db.Rooms.Count();
            //todo...
            //ViewBag.TotalBookings = ...

            //Pune in ViewBag Email-ul utilizatorului conectat
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;

            //MyModel db = new MyModel();

            //Fac cate un ViewBag pentru firstname, lastname, username ( ca sa le folosesc in view )
            var usr = (from u in db.Users
                       where u.EmailID == nume
                       select u).FirstOrDefault();

            if (usr != null)
            {
                ViewBag.FirstName = usr.FirstName;
                ViewBag.LastName = usr.LastName;
                ViewBag.UserName = usr.Username;

            }


            return View();
        }

        

        public ActionResult Settings()
        {
            //Face acelasi lucru ca si mai sus, dar nu stiu daca e neaparat necesar.
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;
            MyModel db = new MyModel();
            var usr = (from u in db.Users
                       where u.EmailID == nume
                       select u).FirstOrDefault();

            if (usr != null)
            {
                ViewBag.FirstName = usr.FirstName;
                ViewBag.LastName = usr.LastName;
                ViewBag.UserName = usr.Username;

            }

            return View();
        }

        //Mergeeeeeeeeeee
        [HttpPost]
        public ActionResult Settings(ChangeAccountDetailsModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                MyModel dc = new MyModel();
                MyModel db = new MyModel();
                ViewBag.EmailID = Session["name"];
                String nume;
                nume = ViewBag.EmailID;
                var usr = (from u in dc.Users
                           where u.EmailID == nume
                           select u).FirstOrDefault();

                if (usr != null)
                {
                    usr.FirstName = model.FirstName;
                    usr.LastName = model.LastName;
                    usr.Username = model.Username;
                    ViewBag.FirstName = usr.FirstName;
                    ViewBag.LastName = usr.LastName;
                    ViewBag.UserName = usr.Username;
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();

                }

                else ViewBag.Status = "Nu a gasit email!";

                return View();
            }

            return View();
        }


      
    }

}