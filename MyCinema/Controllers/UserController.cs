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
                    ViewBag.Messgae = message;
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
                    ViewBag.Messgae = message;
                    TempData["ErrorMessage"] = message;
                    return View();
                }
                user.IsEmailVerified = false;

                byte[] array = System.IO.File.ReadAllBytes(@"C:\Users\Dragos\Desktop\MyCinemaFinal\MyCinema\images\noimg.jpg");
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
            ViewBag.Messgae = message;
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

    }

}