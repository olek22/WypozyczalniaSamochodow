using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using projektWypozyczalnia_Gola.Models;

namespace projektWypozyczalnia_Gola.Controllers
{
    public class UserController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            #region // Creating Pracowniks List
            using (WypAutEntities dc = new WypAutEntities())
            {
                var pracownicy = dc.pracowniks.ToList();
                var list = new List<int>();
                foreach (var c in pracownicy)
                {
                    list.Add(c.id);
                }

                ViewBag.list = list;
            }
            #endregion

            return View();
        }
        //Registration POST action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            bool Status = false;
            string message = "";

            // Model Validation
            if (ModelState.IsValid)
            {
              

                #region // Email is alreadyexist
                var isExist = IsEmailExist(user.Email);
                if(isExist)
                {
                    ModelState.AddModelError("EmailExist", "Emial already exist");
                    return View(user);
                }
                #endregion

                #region// Generowanie kodu aktywacyjnego
                user.ActivationCode = Guid.NewGuid(); // generowanie hasła
                #endregion

                #region// Szyfrowanie hasła
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
                #endregion

                user.IsEmailVerified = false;

                #region// Zapis danych do bazy danych
                using (WypAutEntities dc = new WypAutEntities())
                {
                    dc.Users.Add(user);
                    dc.SaveChanges();

                    //Wysłanie maila do osoby rejestrującej się
                    SendVerificationLinkEmail(user.Email.ToString(), user.ActivationCode.ToString());
                    message = "Rejestrcja zakończona sukcesen. Link aktywacyjny został wysłany na Twój email: "
                    + user.Email;
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Błąd rejestracji";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }

        //Weryfikcja konta

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (WypAutEntities dc = new WypAutEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;// avoid confirm passwor does not match issue on save changes 
                var v = dc.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if ( v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = true;
            return View();
        }
        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";
            using (WypAutEntities dc = new WypAutEntities())
            {
                var v = dc.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                if ( v!=null)
                {
                    if(string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600min = 1 year
                        var ticket = new System.Web.Security.FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if(Url.IsLocalUrl(ReturnUrl)){ return Redirect(ReturnUrl);}
                        else{return RedirectToAction("Index", "Home");}
                    }
                    else{message = "Błędne hasło";
                    }
                }
                else
                {
                    message = "Błędny Email";
                }

            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [NonAction]
        public bool IsEmailExist(string emaildID)
        {
            using (WypAutEntities dc = new WypAutEntities())
            {
                var v = dc.Users.Where(a => a.Email == emaildID).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/"+emailFor+"/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("golaTestApp@gmail.com", "Twórca aplickacji Aleksander Gola");
            //var toEmail = new MailAddress(emailID);
            //var fromEmailPassword = "Test123!"; // aktualne hasło

            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = System.Configuration.ConfigurationManager.AppSettings["formEmailPassword"].ToString();

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Resetowanie hasła";

                body = "<br/><br/> Dostaliśmy zgłoszenie o zapomnienu przez Ciebie hasła. " +
                    "Wejdź w link poniżej żeby zrestartować hasło" + " <br/><br/><a href='" + link + "'>" + link +
                    "</a> ";
                subject = "Twoje konto zostało stworzone";
                body = "<br/><br/> Cieszę się, że udało Ci się stworzyć konto." +
                    "Wejdź w link poniżej żeby zweryfikwać konto" + " <br/><br/><a href='" + link + "'>" + link +
                    "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Resetowanie hasła";

                body = "<br/><br/> Dostaliśmy zgłoszenie o zapomnienu przez Ciebie hasła. " +
                    "Wejdź w link poniżej żeby zrestartować hasło" + " <br/><br/><a href='" + link + "'>" + link +
                    "</a> ";
            }

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, 
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address,fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

            smtp.Send(message);


        }

        // forgot password
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //Verify Email
            // Generate Reset password link
            //Send Email
            string message = "";
            bool status = false;

            using (WypAutEntities dc = new WypAutEntities())
            {
                var account = dc.Users.Where(a => a.Email == Email).FirstOrDefault();
                if (account != null)
                {
                    //Send email for Reset Password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
                    account.ResetPasswordCode = resetCode;
                    // This line I have added here to avoid confirm password not match issue, ass we had added
                    // a confirm password property in our class
                    dc.Configuration.ValidateOnSaveEnabled = false;
                    dc.SaveChanges();
                    message = "Wysłano link resetujący hasło na podany adres email";
                }
                else
                {
                    message = "Nie znaleziono konta";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string id)
        {
            //Verify the link password link
            //Find account associated with this link
            //Redirect to reset password page
            using (WypAutEntities dc = new WypAutEntities())
            {
                var user = dc.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if(user!=null)
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
            if(ModelState.IsValid)
            {
                using (WypAutEntities dc = new WypAutEntities())
                {
                    var user = dc.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        dc.Configuration.ValidateOnSaveEnabled = false;
                        dc.SaveChanges();
                        message = "Zresetowano hasło pomyślnie";
                    }
                }
                ViewBag.Message1 = message;
            }
            else
            {
                message = "Coś poszło nie tak..";
                ViewBag.Message2 = message;
            }
            return View(model);

        }
    }
}