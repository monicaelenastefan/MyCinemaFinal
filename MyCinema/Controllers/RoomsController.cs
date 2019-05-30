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
    public class RoomsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Rooms
        public ActionResult Index()
        {
            MyModel db = new MyModel();
            var nume = Session["name"].ToString();
            var item = (from d in db.Rooms
                        select d).ToList();
            ViewData["Rooms"] = db.Rooms.ToList();
            var Reservation = (from u in db.Reservations
                               where u.Email == nume.ToString()
                               select u).ToList();

            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Reservations = Reservation;
            return View(item);
        }

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
            return View(db.Rooms.ToList());

        }


        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string Name, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
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

            Rooms model = new Rooms();
            string filename = "";

            byte[] bytes;

            int BytestoRead;

            int numBytesRead;

            if (Image1 != null)

            {

                filename = Path.GetFileName(Image1.FileName);

                bytes = new byte[Image1.ContentLength];

                BytestoRead = (int)Image1.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image1.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                model.Image1 = bytes;

            }

            if (Image2 != null)

            {

                filename = Path.GetFileName(Image1.FileName);

                bytes = new byte[Image2.ContentLength];

                BytestoRead = (int)Image2.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image2.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                model.Image2 = bytes;

            }

            if (Image3 != null)

            {

                filename = Path.GetFileName(Image3.FileName);

                bytes = new byte[Image3.ContentLength];

                BytestoRead = (int)Image3.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image3.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                model.Image3 = bytes;

            }

            if (Image4 != null)

            {

                filename = Path.GetFileName(Image4.FileName);

                bytes = new byte[Image4.ContentLength];

                BytestoRead = (int)Image4.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image4.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                model.Image4 = bytes;

            }
            model.RoomName = Name;
           

            db.Rooms.Add(model);
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");



        }

        // GET: Rooms/Edit/5
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
            Rooms rooms = db.Rooms.Find(id);
            if (rooms == null)
            {
                return HttpNotFound();
            }
            return View(rooms);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Edit([Bind(Exclude = "RoomName,Image1,Image2,Image3,Image4")] Rooms rooms, string RoomName, HttpPostedFileBase Image1, HttpPostedFileBase Image2, HttpPostedFileBase Image3, HttpPostedFileBase Image4)
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


            int RoomId = (int)TempData["RoomId"];
            var RoomData = (from u in db.Rooms
                             where u.RoomId == RoomId
                             select u).FirstOrDefault();
            string filename = "";

            byte[] bytes;

            int BytestoRead;

            int numBytesRead;

            if (Image1 != null)

            {

                filename = Path.GetFileName(Image1.FileName);

                bytes = new byte[Image1.ContentLength];

                BytestoRead = (int)Image1.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image1.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                RoomData.Image1 = bytes;
            }

            if (Image2 != null)

            {

                filename = Path.GetFileName(Image2.FileName);

                bytes = new byte[Image2.ContentLength];

                BytestoRead = (int)Image2.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image2.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                RoomData.Image2 = bytes;
            }

            if (Image3 != null)

            {

                filename = Path.GetFileName(Image3.FileName);

                bytes = new byte[Image3.ContentLength];

                BytestoRead = (int)Image3.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image3.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                RoomData.Image3 = bytes;
            }

            if (Image4 != null)

            {

                filename = Path.GetFileName(Image4.FileName);

                bytes = new byte[Image4.ContentLength];

                BytestoRead = (int)Image4.ContentLength;

                numBytesRead = 0;

                while (BytestoRead > 0)

                {

                    int n = Image4.InputStream.Read(bytes, numBytesRead, BytestoRead);

                    if (n == 0) break;

                    numBytesRead += n;

                    BytestoRead -= n;

                }

                RoomData.Image4 = bytes;
            }

            if (RoomName != null)
            {
                RoomData.RoomName = RoomName;
            }

            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            return RedirectToAction("IndexAdmin");
        }


        [HttpPost]
        public ActionResult Delete()
        {
            return RedirectToAction("IndexAdmin");
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> RoomIdDelete)
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

            List<Rooms> list = db.Rooms.Where(x => RoomIdDelete.Contains(x.RoomId)).ToList();
            foreach (Rooms item in list)
            {
                db.Rooms.Remove(item);
            }

            db.SaveChanges();

            return RedirectToAction("IndexAdmin");

        }

    }



}
