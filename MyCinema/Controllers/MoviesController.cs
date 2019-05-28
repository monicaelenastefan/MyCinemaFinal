using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;
using System.IO;


namespace MyCinema.Controllers
{
    public class MoviesController : Controller
    {
        private MyModel db = new MyModel();


        public ActionResult Index()
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

        // GET: Movies
        public ActionResult IndexAdmin()
        {
            

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
            return View(db.Movies.ToList());

        }

        public ActionResult MyImage(int id, int img)

        {

            ViewBag.Pic = img;

            Movies insp = db.Movies.Find(id);

            return View(insp);

        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        
       
        [HttpGet]
        public ActionResult Create()
        {
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
            Movies m1 = new Movies();
            return View(m1);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

            
        [HttpPost]
       
        public ActionResult Create( string Name, HttpPostedFileBase Image, double Price, string Hours, string Minutes, string Seconds)
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

            Movies model = new Movies();
            string filename = "";

            byte[] bytes;

            int BytestoRead;

            int numBytesRead;

            if (Image != null)

            { 

                filename = Path.GetFileName(Image.FileName);

                bytes = new byte[Image.ContentLength];

                BytestoRead = (int)Image.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

               model.Image = bytes;

            }
            model.Name = Name;
            model.Price = Price;
            if( Hours.Length == 1)
            {
                Hours = "0" + Hours;
            }
            if (Minutes.Length == 1)
            {
                Minutes = "0" + Minutes;
            }
            if (Seconds.Length == 1)
            {
                Seconds = "0" + Seconds;
            }
            model.Duration = Hours+":"+Minutes+":"+Seconds;
           
                db.Movies.Add(model);
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {

            //Pune in ViewBag Email-ul utilizatorului conectat
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;
            ViewBag.MovieId = id;

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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(string Name, double Price, string Duration, HttpPostedFileBase Image)
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


            int MovieId = (int)TempData["MovieId"];
            var MovieData = (from u in db.Movies
                             where u.MovieId == MovieId
                             select u).FirstOrDefault();



            string filename = "";

            byte[] bytes;

            int BytestoRead;

            int numBytesRead;

            if (Image != null)
            {

                filename = Path.GetFileName(Image.FileName);

                bytes = new byte[Image.ContentLength];

                BytestoRead = (int)Image.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }


                MovieData.Image = bytes;

            }
            if (Name != null)
                MovieData.Name = Name;

            if( Price > 0.0)
             MovieData.Price = Price;
            if (Duration != null)
                MovieData.Duration = Duration;

            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            return RedirectToAction("IndexAdmin");


        }

        // GET: Movies/Delete/5
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> MovieIdDelete)
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

            List<Movies> list= db.Movies.Where(x => MovieIdDelete.Contains(x.MovieId)).ToList();
            foreach(Movies item in list)
            {
                db.Movies.Remove(item);
            }

            db.SaveChanges();

            return RedirectToAction("IndexAdmin");
          
        }

        // POST: Movies/Delete/5
   

        

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
