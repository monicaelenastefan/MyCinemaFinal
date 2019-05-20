using MyCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCinema.Controllers
{
    public class CinemaRoomsController : Controller
    {
        // GET: AddRoom
      
        public ActionResult Rooms()
        {
            MyModel db = new MyModel();
            var item = (from d in db.Rooms
                        select d).ToList();
            ViewData["Rooms"] = db.Rooms.ToList();
            return View(item);
        }
    }
}