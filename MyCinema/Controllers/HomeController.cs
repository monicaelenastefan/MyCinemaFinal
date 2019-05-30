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

            var Reservation = (from u in db.Reservations
                               where u.Email == nume.ToString()
                               select u).ToList();

            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Reservations = Reservation;

            return View();
        }

        // GET: Home
        public ActionResult Admin(string name)
        {
            MyModel db = new MyModel();

            int LastUserId = db.Users.DefaultIfEmpty().Max(p => p == null ? 0 : p.UserId);
            int LastRoomId = db.Rooms.DefaultIfEmpty().Max(p => p == null ? 0 : p.RoomId);
            int LastMovieId= db.Movies.DefaultIfEmpty().Max(p => p == null ? 0 : p.MovieId);
           
            var LastUser = (from u in db.Users
                       where u.UserId == LastUserId
                            select u).FirstOrDefault();
            ViewBag.LastUser = LastUser;
            ViewBag.TotalUsers = db.Users.Count();
            ViewBag.TotalMovies = db.Movies.Count();
            ViewBag.TotalRooms = db.Rooms.Count();
            ViewBag.TotalBookings = db.Reservations.Count();

            var LastRoom = (from u in db.Rooms
                           where u.RoomId == LastRoomId
                            select u).FirstOrDefault();

            ViewBag.LastRoom = LastRoom;

            var LastMovie = (from u in db.Movies
                            where u.MovieId == LastMovieId
                            select u).FirstOrDefault();

            ViewBag.LastMovie = LastMovie;
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
                ViewBag.Email = usr.EmailID;
                ViewBag.PhoneNumber = usr.PhoneNumber;
                ViewBag.BirthDay = usr.BirthDay;

            }
            var Reservation = (from u in db.Reservations
                               where u.Email == nume.ToString()
                               select u).ToList();

            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Reservations = Reservation;

            return View();
        }

        //Mergeeeeeeeeeee
        [HttpPost]
        public ActionResult Settings(ChangeAccountDetailsModel model)
        {
            var message = "";
            ViewBag.LastName = "Invalid model...";
          
                MyModel dc = new MyModel();
                MyModel db = new MyModel();
                ViewBag.Email = Session["name"];
                String nume;
                nume = ViewBag.Email;
                var usr = (from u in dc.Users
                           where u.EmailID == nume
                           select u).FirstOrDefault();
                ViewBag.LastName = "User not found...";
                if (usr != null)
                {
                    if(model.FirstName != null)
                        usr.FirstName = model.FirstName;
                    if(model.LastName != null)
                     usr.LastName = model.LastName;

                    usr.BirthDay = model.BirthDay;
                    
                    if (model.Email != null)
                        usr.EmailID = model.Email;
                    if (model.PhoneNumber != null)
                        usr.PhoneNumber = model.PhoneNumber;
                if (model.NewPassword != null)
                {
                    usr.Password =Crypto.Hash(model.NewPassword);
                }



                    ViewBag.FirstName = usr.FirstName;
                    ViewBag.LastName = usr.LastName;
                    ViewBag.Email = usr.EmailID;
                    ViewBag.PhoneNumber = usr.PhoneNumber;
                    ViewBag.BirthDay = usr.BirthDay;
                    Session["name"] = usr.EmailID;

                    Session["FirstName"] = usr.FirstName;
                    Session["LastName"] = usr.LastName;


                dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();

                }

                else ViewBag.Status = "Nu a gasit email!";

            var Reservation = (from u in db.Reservations
                               where u.Email == nume.ToString()
                               select u).ToList();

            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Reservations = Reservation;

            return View();
       
        }


      
    }

}