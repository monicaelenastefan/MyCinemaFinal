using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MyCinema.Models;
using System.Web.Security;
using System.IO;


namespace MyCinema.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]


        public ActionResult IndexAdmin()
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
            return View(db.Users.ToList());

        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")]Users user)
        {
            bool Status = false;
            string message = null;
            ViewBag.Message = message;

            if (ModelState.IsValid)
            {
                #region //Email is already exist
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {

                    message = "Email already exists";
                    ViewBag.Messgae = message;
                    TempData["ErrorMessage"] = message;
                    return View();
                }
                #endregion
                if (user.Password != user.ConfirmPassword)
                {
                    message = "Passwords do not match";
                    ViewBag.Message = message;
                    TempData["ErrorMessage"] = message;
                    return View();
                }
                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion
                if(user.Password != user.ConfirmPassword)
                {
                    message = "Passwords do not match";
                    ViewBag.Message = message;
                    TempData["ErrorMessage"] = message;
                    return View();
                }
                user.IsEmailVerified = false; 

                byte[] array = System.IO.File.ReadAllBytes(@"C: \Users\Monica\source\repos\MyCinemaFinal\MyCinema\images\noimg2.png");
                user.Image = array;
                #region Save to Database
                using (MyModel dc = new MyModel())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        "has been send to your email id: " + user.EmailID;
                    Status = true;

                }
                #endregion
            }
            else
            {
                message = "Invalid Request. ";
            }
            ViewBag.Message = message;
            ViewBag.Status = Status;
            TempData["ErrorMessage"] = message;
            return View(user);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (MyModel dc = new MyModel())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;

                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid requiest  ";
                }
            }
            ViewBag.Status = true;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            ViewBag.Message = message;
            using (MyModel dc = new MyModel())
            {
                var v = dc.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                            if (v.EmailID == "tampu.andra@yahoo.ro")
                        {
                            Session["name"] = login.EmailID;
                            if (Session["name"] != null)
                                return RedirectToAction("Admin", "Home", new { EmailID = Session["name"].ToString() });
                        }
                        else
                        {
                            Session["name"] = login.EmailID;
                            if (Session["name"] != null)
                                return RedirectToAction("Index", "Home", new { EmailID = Session["name"].ToString() });

                            //   return RedirectToAction("Index", "Home");

                        }

                    }
                    else
                    {
                        message = "Invalid Password";

                    }
                }
                else
                {
                    message = "Invalid email";

                }
            }
            ViewBag.Messgae = message;
            TempData["ErrorMessage"] = message;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (MyModel dc = new MyModel())
            {
                var v = dc.Users.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var vefifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, vefifyUrl);

            var fromEmail = new MailAddress("diiadiana13@gmail.com", "Validation User");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "andradelia99";

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {

                subject = "Your account is succesfullycreated!";

                body = "<br/><br/> We are excited to tell you that your account is " +
                   "succesfully created. Please click on the below link to verify your account" +
                   "<br/><br/><a href='" + link + "'>" + link + "</a>";
            }

            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/><br/> We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link <a/>";

            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string message = "";
            bool status = false;

            using (MyModel dc = new MyModel())
            {
                var account = dc.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                if (account != null)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EmailID, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;

                    dc.Configuration.ValidateOnSaveEnabled = false;

                    dc.SaveChanges();

                    message = "Reset password link has been sent to your email id";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            using (MyModel dc = new MyModel())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (MyModel dc = new MyModel())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "New password update successfully";
                    }
                }
            }
            else
            {
                message = "Something invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> UserIdDelete)
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

            List<Users>list = db.Users.Where(x => UserIdDelete.Contains(x.UserId)).ToList();
            foreach (Users item in list)
            {
                db.Users.Remove(item);
            }

            db.SaveChanges();

            return RedirectToAction("IndexAdmin");
          
        }

        public ActionResult Edit(int? id)
        {

            //Pune in ViewBag Email-ul utilizatorului conectat
            ViewBag.EmailID = Session["name"];
            String nume;
            nume = ViewBag.EmailID;
            ViewBag.MovieId = id;

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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Exclude = "RoomName,FirstName, LastName, Username, PhoneNumber , Image")]  string FirstName, string LastName, string Username , string PhoneNumber, HttpPostedFileBase Image)
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
                ViewBag.EmailID = usr.EmailID;

            }

          
            int UserId = (int)TempData["UserId"];
            var UserData = (from u in db.Users
                             where u.UserId == UserId
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


                UserData.Image = bytes;

            }
            if (FirstName != null)
                UserData.FirstName = FirstName;
            if (LastName != null)
                UserData.LastName = LastName;
            if (Username != null)
                UserData.Username =Username;
            if (PhoneNumber != null)
                UserData.PhoneNumber = PhoneNumber;

            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();

            return RedirectToAction("IndexAdmin");


        }

    }

}
