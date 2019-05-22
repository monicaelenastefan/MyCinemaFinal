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

        // GET: Movies
        public ActionResult Index()
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


       
        // GET: Movies/Create
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
                return RedirectToAction("Index");
            

           
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Name,Image,Price,MinimalAge,ReleaseDate,Duration")] Movies movies)
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
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Pune in ViewBag Email-ul utilizatorului conectat

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
            Movies movies = db.Movies.Find(id);
            db.Movies.Remove(movies);
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
