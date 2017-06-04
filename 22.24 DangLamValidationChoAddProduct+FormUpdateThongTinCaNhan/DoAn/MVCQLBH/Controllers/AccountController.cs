using BotDetect.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;
using System;
using System.Collections.Generic;
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
                if(user != null)
                {
                    ui.Permission = user.f_Permission;
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

        //Ajax Register
        //[HttpPost]
        //[AllowAnonymous]
        //[CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]
        //public ActionResult AjaxRegister(Registing Reg)
        //{
        //    //Check null?
        //    RegisterError l = new RegisterError();
        //    l = AddHelpers.GetErrorNullRegisting(Reg);

        //    //No Error
        //    if (l != null)
        //    {
        //        Thread.Sleep(1000);
        //        return Json(l);
        //    }
        //    else
        //    {
        //        RegisterError ll = new RegisterError();
        //        //Check Captcha
        //        if (!ModelState.IsValid)
        //        {
        //            //
        //            //MvcCaptcha.ResetCaptcha("ExampleCaptcha");
        //            ll.ErrorCaptcha = "Không đúng Captcha";
        //            return Json(ll);

        //        }
        //        else
        //        {
        //            MvcCaptcha.ResetCaptcha("ExampleCaptcha");
        //            //
        //            var u = new Users
        //            {
        //                f_Username = Reg.Username,
        //                f_Password = Ulti.Md5Hash(Reg.Password),
        //                f_Name = Reg.Name,
        //                f_Email = Reg.Email,
        //                f_DOB = DateTime.ParseExact(Reg.DOB, "dd/m/yyyy", null)
        //            };
        //            using (var dc = new QLBHEntities())
        //            {
        //                dc.Users.Add(u);
        //                dc.SaveChanges();
        //            }
        //            var ui = new UserInfo
        //            {
        //                Username = u.f_Username,
        //                Password = u.f_Password
        //            };
        //            Session["Logged"] = ui;
        //            Thread.Sleep(1000);
        //            return Json(ll);
        //        }
        //    }
        //}

        [AuthActionFilter]
        
//#pragma warning disable CS0108 
//        // 'AccountController.Profile()' hides inherited member 'Controller.Profile'. Use the new keyword if hiding was intended.
        public ActionResult Profile()
//#pragma warning restore CS0108 
//            // 'AccountController.Profile()' hides inherited member 'Controller.Profile'. Use the new keyword if hiding was intended.
        {
            //if(Session["Logged"] == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            var ui = Session["Logged"] as UserInfo;
            return View(ui);
        }

    }
}


