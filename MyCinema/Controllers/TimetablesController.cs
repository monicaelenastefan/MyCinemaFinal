﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;

namespace MyCinema.Controllers
{
    public class TimetablesController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Timetables
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
            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            return View(db.Timetables.ToList());
        }

        // GET: Timetables/Details/5
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
            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.Timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // GET: Timetables/Create
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
            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            return View();
        }

        // POST: Timetables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,MovieId,RoomId,Date,StartTime")] Timetable timetable)
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
            
           for ( int i=0; i < 169; i++)
            {
                timetable.Matrix = timetable.Matrix + '0';
            }

            if (ModelState.IsValid)
            {
                db.Timetables.Add(timetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable);
        }

        // GET: Timetables/Edit/5
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
            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Rooms = db.Rooms.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.Timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // POST: Timetables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,MovieId,RoomId,Date,StartTime")] Timetable timetable)
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

          //  int[,] amn = new int[13, 13];
            /*for(int i=0;i<=13;i++)
            {
                for (int j = 0; j <= 13; j++)
                    amn[i, j] = 0;
            }*/
          //  timetable.Matrix = amn;
            

            if (ModelState.IsValid)
            {
                db.Entry(timetable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable);
        }

        // GET: Timetables/Delete/5
    

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> idDelete)
        {
            //Pune in ViewBag Email-ul utilizatorului conectat
          

            List<Timetable> list = db.Timetables.Where(x => idDelete.Contains(x.id)).ToList();
            foreach (Timetable item in list)
            { 
            
                db.Timetables.Remove(item);
            }

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


        [HttpGet]
        public ActionResult DisplayProgram(DateTime date)
        {
            MyModel model = new MyModel();
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
            if (date.Equals(null))
            {
                date = DateTime.Now;
            }
            
            List<string> moviesToday = new List<string>();
            foreach (var item in model.Timetables)
            {
                
                if(item.Date.ToString("dd-MM-yyyy").Equals(date.ToString("dd-MM-yyyy")))
                {
                    if(!moviesToday.Contains(model.Movies.Find(item.MovieId).Name))
                        moviesToday.Add(model.Movies.Find(item.MovieId).Name);
                }
            }


            var map = new Dictionary<string, Tuple<int,List<TimeSpan>>>();
            
            foreach (var movie in moviesToday)
            {
                List<TimeSpan> hours = new List<TimeSpan>();
                int x = 0;

                foreach(var item in model.Timetables)
                {
                    if (item.Date.ToString("dd-MM-yyyy").Equals(date.ToString("dd-MM-yyyy")))
                        if(movie.Equals(model.Movies.Find(item.MovieId).Name))
                        
                            hours.Add(item.StartTime);
                    TempData["RoomName"]=model.Rooms.Find(item.RoomId).RoomName.ToString();
                    ViewBag.room = model.Rooms.Find(item.RoomId).RoomName.ToString();
                    TempData["Price"] = model.Movies.Find(item.MovieId).Price.ToString();
                    ViewBag.price = model.Movies.Find(item.MovieId).Price.ToString();
                    TempData["MovieName"] = movie;
                    ViewBag.time = item.StartTime;
                    x = item.MovieId;
                    
                }

                var tuple = new Tuple<int, List<TimeSpan>>(x, hours);

                map.Add(movie, tuple);
                ViewBag.movie = movie;
                
            }
            ViewBag.date = date;
            ViewBag.map = map;

            return View();
        }

        [HttpGet]
        public ActionResult BookTicket(int? id)
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
                ViewBag.email = usr.EmailID;

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.Timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        [HttpGet]
        public ActionResult BookTicket2(string row,string column)
        {
            Reservations reservation = new Reservations();

            reservation.Email = ViewBag.email;
            reservation.Movie = ViewBag.movie;
            reservation. Room = ViewBag.room;
            reservation.Day = ViewBag.date;
            reservation.Hour = ViewBag.time;
            reservation.Row = Int32.Parse(row);
            reservation.Column = Int32.Parse(column);
            reservation.Price = ViewBag.price;
            

            db.Reservations.Add(reservation);
            db.SaveChanges();

            return View();
        }
    }
}
