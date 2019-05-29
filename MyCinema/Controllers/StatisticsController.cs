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
    public class StatisticsController : Controller
    {
        private MyModel db = new MyModel();
        // GET: Statistics
        public ActionResult Index()
        {
            long UsersUnder12 = 0;
            long UsersUnder15 = 0;
            long UsersUnder18 = 0;
            long UsersAbove18 = 0;

            var today = DateTime.Today;

            foreach (var User in db.Users)
            {
                var Age = today.Year - User.BirthDay.Year;
                if (User.BirthDay.Date > today.AddYears(-Age))
                    Age--;
                //check if Birthday is set in db(default:01/01/0001)
                if (Age < 120)
                {
                    //code
                    if (Age >= 18)
                        UsersAbove18++;
                    else if (Age >= 15)
                        UsersUnder18++;
                    else if (Age >= 12)
                        UsersUnder15++;
                    else
                        UsersUnder12++;
                }
            }
            //trimit datele despre users catre View
            ViewBag.Under12 = UsersUnder12;
            ViewBag.Under15 = UsersUnder15;
            ViewBag.Under18 = UsersUnder18;
            ViewBag.Adults = UsersAbove18;

            ///
            int[] Movies = new int[14];


            var Today = DateTime.Today.DayOfYear;
            foreach(var Movie in db.Timetables)
            {
                int Days = (Today - Movie.Date.DayOfYear) % 365;
                if (Days < 14 && Days >= 0)
                    Movies[Days]++;
            }
            float Max = (float)Movies.Max();
            ViewBag.Movies0 =(int)( (float)(Movies[0]/Max) * 100);
            ViewBag.Movies1 = (int)((float)(Movies[1] / Max) * 100);
            ViewBag.Movies2 = (int)((float)(Movies[2] / Max) * 100);
            ViewBag.Movies3 = (int)((float)(Movies[3] / Max) * 100);
            ViewBag.Movies4 = (int)((float)(Movies[4] / Max) * 100);
            ViewBag.Movies5 = (int)((float)(Movies[5] / Max) * 100);
            ViewBag.Movies6 = (int)((float)(Movies[6] / Max) * 100);
            ViewBag.Movies7 = (int)((float)(Movies[7] / Max) * 100);
            ViewBag.Movies8 = (int)((float)(Movies[8] / Max) * 100);
            ViewBag.Movies9 = (int)((float)(Movies[9] / Max) * 100);
            ViewBag.Movies10 = (int)((float)(Movies[10] / Max) * 100);
            ViewBag.Movies11 = (int)((float)(Movies[11] / Max) * 100);
            ViewBag.Movies12 = (int)((float)(Movies[12] / Max) * 100);
            ViewBag.Movies13 = (int)((float)(Movies[13] / Max) * 100);
            int MoviesThisWeek = Movies[0] + Movies[1] + Movies[2] + Movies[3] + Movies[4] + Movies[5] + Movies[6];
            int MoviesLastWeek = Movies[7] + Movies[8] + Movies[9] + Movies[10] + Movies[11] + Movies[12] + Movies[13];
            ViewBag.MoviesThisWeek = MoviesThisWeek;
            ViewBag.MoviesLastWeek = MoviesLastWeek;
            float Percentage = ((float)(MoviesThisWeek - MoviesLastWeek) / (float)MoviesLastWeek) * 100;
            ViewBag.Percentage = Percentage;

            return View();
        }
      
    }
}