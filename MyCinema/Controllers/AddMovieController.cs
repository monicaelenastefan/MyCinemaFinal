using MyCinema.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCInemaMovies.Controllers
{
    public class AddMovieController : Controller
    {

        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMovie()
        {
            Movies m1 = new Movies();
            return View(m1);
        }

        [HttpPost]
        public ActionResult AddMovie(Movies model, HttpPostedFileBase ImageData)
        {
            MyModel db = new MyModel();
            if (ImageData != null)
            {
               // model.Image = new byte[ImageData.ContentLength];
                //ImageData.InputStream.Read(model.Image, 0, ImageData.ContentLength);

            }

            db.Movies.Add(model);
            db.SaveChanges();
            return View(model);
        }


    }
}