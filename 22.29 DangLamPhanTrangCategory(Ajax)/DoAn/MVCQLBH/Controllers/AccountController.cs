using BotDetect.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Controllers
{
    //[AuthActionFilter]
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POSST: Account/Login
        [HttpPost]
        public ActionResult Login(UserInfo ui)
        {
            var pass = Ulti.Md5Hash(ui.Password);
            using (var dc = new QLBHEntities())
            {
                var user = dc.Users.Where(u => u.f_Username == ui.Username && u.f_Password == pass).FirstOrDefault();
                if (user != null)
                {
                    ui.Permission = user.f_Permission;
                    ui.UserID = user.f_ID;
                    Session["Logged"] = ui;
                    Response.Cookies["UserId"].Value = user.f_ID.ToString();
                    Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(7);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMsg = "Thông tin đăng nhập chưa đúng";
                }
            }
            return View();
        }

        //POST: Account/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session["Logged"] = null;
            Session["cart"] = null;
            Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            var u = new UserRegisting
            {
                Username = "user001",
                Password = "1234",
                PasswordRetype = "1234",
                Name = "user 0009",
                DOB = "12/6/1999",
            };
            return View(u);
            //return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Register(UserRegisting user)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                //ViewBag.ErrorMsg = "Incorrect CAPTCHA code!";
                ViewBag.ErrorMsg = "CAPTCHA không hợp lệ!";
            }
            else
            {
                using (var dc = new QLBHEntities())
                {
                    var userTonTai = dc.Users.Where(u => u.f_Username == user.Username).FirstOrDefault();
                    if (userTonTai != null)
                    {
                        ViewBag.ErrorMsg = "Tên đăng nhập đã tồn tại";
                    }
                    else
                    {
                        var u = new User
                        {
                            f_Username = user.Username,
                            f_Password = Ulti.Md5Hash(user.Password),
                            f_Name = user.Name,
                            f_Email = user.Email,
                            f_DOB = DateTime.ParseExact(user.DOB, "d/m/yyyy", null)
                        };
                        dc.Users.Add(u);
                        dc.SaveChanges();

                        //ViewBag.ErrorMsg = "Đăng kí tài khoản thành công!";
                        var ui = new UserInfo { Username = u.f_Username };
                        Session["Logged"] = ui;
                        return RedirectToAction("Index", "Home");

                        //return View("RegisterSuccess", user);
                        // return RedirectToAction("RegisterSuccess", "Home");
                    }
                }

            }
            return View();
        }

        // GET: Account/Register
        public ActionResult AjaxRegister()
        {
            var u = new UserRegisting
            {
                Username = "user001ajax",
                Password = "1234",
                PasswordRetype = "1234",
                Name = "user 0009 ajax",
                DOB = "12/6/1999",
            };
            return View(u);
            //return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult AjaxRegister(UserRegisting ur)
        {
            // Kiểm tra tất cả thông tin của ur truyền vào có cái nào null không 
            RegisterError l = new RegisterError();
            l = AddHelpers.GetErrorNullRegisting(ur);

            //No Error
            if ((l.ErrorUsername != null || l.ErrorPassword != null || l.ErrorPasswordRetype != null || l.ErrorName != null || l.ErrorEmail != null || l.ErrorDOB != null) || ModelState.IsValid == false)
            {
                //Check Captcha
                if (!ModelState.IsValid)
                {
                    l.ErrorCaptcha = "Không đúng Captcha";
                    Thread.Sleep(1000);
                    MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                    return Json(l);
                }
                Thread.Sleep(1000);
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                return Json(l);
            }
            else
            {
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                //
                var u = new User
                {
                    f_Username = ur.Username,
                    f_Password = Ulti.Md5Hash(ur.Password),
                    f_Name = ur.Name,
                    f_Email = ur.Email,
                    f_DOB = DateTime.ParseExact(ur.DOB, "dd/m/yyyy", null)
                };
                using (var dc = new QLBHEntities())
                {
                    dc.Users.Add(u);
                    dc.SaveChanges();
                }
                var ui = new UserInfo
                {
                    Username = u.f_Username,
                    Password = u.f_Password
                };

                //var ui = new UserInfo { Username = u.f_Username };
                Session["Logged"] = ui;
                Thread.Sleep(1000);
                return Json(l);
            }
        }

        [AuthActionFilter]
        public ActionResult Profile()
        {
            //if(Session["Logged"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            var ui = Session["Logged"] as UserInfo;
            using (var dc = new QLBHEntities())
            {
                var u = dc.Users.Where(m => m.f_Name == ui.Username).FirstOrDefault();
                if (u != null)
                {
                    var a = new UserInfo
                    {
                        UserID = u.f_ID,
                        Username = u.f_Name,
                        //Password = u.f_Password,
                        //PasswordRetype = u.f_Password,
                        Name = u.f_Name,
                        Email = u.f_Email,
                        DOB = u.f_DOB,
                    };
                    return View(a);
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateProfile(UserInfo user)
        {
            using (var dc = new QLBHEntities())
            {
                var u = dc.Users.Where(m => m.f_ID == user.UserID).FirstOrDefault();
                if (u != null)
                {
                    u.f_Username = user.Username;
                    //u.f_Password = Ulti.Md5Hash(user.Password);
                    u.f_Name = user.Name;
                    u.f_Email = user.Email;
                    u.f_DOB = user.DOB;

                    var a = dc.Users.Where(m => m.f_Username == u.f_Username).FirstOrDefault();
                    if (a != null)
                    {
                        ViewBag.ErrorMsg = "Tên đăng nhập đã tồn tại";
                    }
                    else
                    {
                        dc.Entry(u).State = EntityState.Modified;
                        dc.SaveChanges();
                        Session["Logged"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View("Profile");
        }

        [AuthActionFilter]
        public ActionResult AjaxProfile()
        {
            var ui = Session["Logged"] as UserInfo;
            using (var dc = new QLBHEntities())
            {
                var u = dc.Users.Where(m => m.f_Name == ui.Username).FirstOrDefault();
                if (u != null)
                {
                    var a = new UserInfo
                    {
                        UserID = u.f_ID,
                        Username = u.f_Name,
                        Name = u.f_Name,
                        Email = u.f_Email,
                        DOB = u.f_DOB,
                    };
                    return View(a);
                }
                return View();
            }
        }

        [HttpPost]
        public ActionResult AjaxUpdateProfile(UserInfo user)
        {
            // Kiểm tra tất cả thông tin của ur truyền vào có cái nào null không 
            ProfileError l = new ProfileError();
            l = AddHelpers.GetErrorNullProfile(user);

            //No Error
            if (l.ErrorUsername != null || l.ErrorName != null || l.ErrorEmail != null || l.ErrorDOB != null)
            {
                Thread.Sleep(10);
                return Json(l);
            }
            else
            {
                var u = new User
                {
                    f_Username = user.Username,
                    f_Name = user.Name,
                    f_Email = user.Email,
                    f_DOB = user.DOB,
                };
                using (var dc = new QLBHEntities())
                {
                    dc.Entry(u).State = EntityState.Modified;
                    dc.SaveChanges();
                    //Session["Logged"] = null;
                    return RedirectToAction("Index", "Home");
                }
                //var ui = new UserInfo
                //{
                //    Username = u.f_Username,
                //    Password = u.f_Password
                //};

                //var ui = new UserInfo { Username = u.f_Username };
                //Session["Logged"] = ui;
                Thread.Sleep(10);
                return Json(l);
            }
            return View("Profile");
        }




    }
}


