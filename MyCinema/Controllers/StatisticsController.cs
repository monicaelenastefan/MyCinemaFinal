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
            var nume = Session["name"].ToString();
            var Res = (from u in db.Reservations
                               where u.Email == nume.ToString()
                               select u).ToList();

            ViewBag.Movies = db.Movies.ToList();
            ViewBag.Reservations = Res;

            //Age distribution
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
            //Movies PLayed
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


            //MovieDistribution
            int Price10 = 0;
            int Price15 = 0;
            int Price20 = 0;
            int Price25 = 0;
            int Price30 = 0;
            int PriceX = 0;


            foreach(var Movie in db.Movies)
            {
                if (Movie.Price <= 10)
                    Price10++;
                else
                    if (Movie.Price <= 15)
                    Price15++;
                else
                    if (Movie.Price <= 20)
                    Price20++;
                else
                    if (Movie.Price <= 25)
                    Price25++;
                else
                    if (Movie.Price <= 30)
                    Price30++;
                else
                    PriceX++;
            }

            ViewBag.Price10 = Price10;
            ViewBag.Price15 = Price15;
            ViewBag.Price20 = Price20;
            ViewBag.Price25 = Price25;
            ViewBag.Price30 = Price30;
            ViewBag.PriceX = PriceX;

            //Total tickets && Total Earnings
            long[] Tickets = new long[14];
            double[] Earnings = new double[14];
            Today = DateTime.Today.DayOfYear;
            foreach (var Reservation in db.Reservations)
            {
                int Days = (Today - Reservation.Day.DayOfYear) % 365;
                if (Days < 14 && Days >= 0)
                {
                    Tickets[Days]++;
                    Earnings[Days] += Reservation.Price;
                }
            }
            ViewBag.TicketsThisWeek = Tickets[0] + Tickets[1] + Tickets[2] + Tickets[3] + Tickets[4] + Tickets[5] + Tickets[6];
            ViewBag.TicketsLastWeek = Tickets[7] + Tickets[8] + Tickets[9] + Tickets[10] + Tickets[11] + Tickets[12] + Tickets[13];
            ViewBag.Tickets0 = Tickets[0];
            ViewBag.Tickets1 = Tickets[1];
            ViewBag.Tickets2 = Tickets[2];
            ViewBag.Tickets3 = Tickets[3];
            ViewBag.Tickets4 = Tickets[4];
            ViewBag.Tickets5 = Tickets[5];
            ViewBag.Tickets6 = Tickets[6];
            ViewBag.Tickets7 = Tickets[7];
            ViewBag.Tickets8 = Tickets[8];
            ViewBag.Tickets9 = Tickets[9];
            ViewBag.Tickets10 = Tickets[10];
            ViewBag.Tickets11 = Tickets[11];
            ViewBag.Tickets12 = Tickets[12];
            ViewBag.Tickets13 = Tickets[13];
            float TicketsPercentage = ((float)(ViewBag.TicketsThisWeek - ViewBag.TicketsLastWeek) / (float)ViewBag.TicketsLastWeek) * 100;
            ViewBag.TicketsPercentage = TicketsPercentage;


            ViewBag.EarningsThisWeek = Earnings[0] + Earnings[1] + Earnings[2] + Earnings[3] + Earnings[4] + Earnings[5] + Earnings[6];
            ViewBag.EarningsLastWeek = Earnings[7] + Earnings[8] + Earnings[9] + Earnings[10] + Earnings[11] + Earnings[12] + Earnings[13];
            ViewBag.Earnings0 = Earnings[0];
            ViewBag.Earnings1 = Earnings[1];
            ViewBag.Earnings2 = Earnings[2];
            ViewBag.Earnings3 = Earnings[3];
            ViewBag.Earnings4 = Earnings[4];
            ViewBag.Earnings5 = Earnings[5];
            ViewBag.Earnings6 = Earnings[6];
            ViewBag.Earnings7 = Earnings[7];
            ViewBag.Earnings8 = Earnings[8];
            ViewBag.Earnings9 = Earnings[9];
            ViewBag.Earnings10 = Earnings[10];
            ViewBag.Earnings11 = Earnings[11];
            ViewBag.Earnings12 = Earnings[12];
            ViewBag.Earnings13 = Earnings[13];
            float EarningsPercentage = ((float)(ViewBag.EarningsThisWeek - ViewBag.EarningsLastWeek) / (float)ViewBag.EarningsLastWeek) * 100;
            ViewBag.EarningsPercentage = EarningsPercentage;



            return View();
        }
      
    }
}