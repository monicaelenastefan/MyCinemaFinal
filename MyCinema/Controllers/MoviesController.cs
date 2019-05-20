using MyCinema.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCInemaMovies.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Movies()
        {
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;
            var db = new MyModel();
            var usr = (from u in db.Users
                       where u.EmailID == nume
                       select u).FirstOrDefault();

            if (usr != null)
            {
                ViewBag.FirstName = usr.FirstName;
                ViewBag.LastName = usr.LastName;
                ViewBag.UserName = usr.Username;

            }

            var item = (from d in db.Movies
                        select d).ToList();
            ViewData["Movies"] = db.Movies.ToList();
            return View(item);
        }
    }
}

    