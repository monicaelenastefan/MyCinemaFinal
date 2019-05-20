using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;

namespace MyCinema.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
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

        public ActionResult AddRoom()
        {
            Rooms m1 = new Rooms();
            return View(m1);
        }

        [HttpPost]
        public ActionResult AddRoom(Rooms model, HttpPostedFileBase ImageData1, HttpPostedFileBase ImageData2, HttpPostedFileBase ImageData3)
        {
            MyModel db = new MyModel();
            if (ImageData1 != null)
            {
                model.RoomImage1 = new byte[ImageData1.ContentLength];
                ImageData1.InputStream.Read(model.RoomImage1, 0, ImageData1.ContentLength);

            }

            if (ImageData2 != null)
            {
                model.RoomImage2 = new byte[ImageData2.ContentLength];
                ImageData2.InputStream.Read(model.RoomImage2, 0, ImageData2.ContentLength);

            }

            if (ImageData3 != null)
            {
                model.RoomImage3 = new byte[ImageData3.ContentLength];
                ImageData3.InputStream.Read(model.RoomImage3, 0, ImageData3.ContentLength);

            }

            db.Rooms.Add(model);

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            //db.SaveChanges();
            return View(model);
        }
    }

}