using BotDetect.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]

        public ActionResult Register(UserRegisting user)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                ViewBag.ErrorMsg = "Incorrect CAPTCHA code!";
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

                using (var dc = new QLBHEntities())
                {
                    dc.Users.Add(u);
                    dc.SaveChanges();
                }
            }
            return View();
        }
    }
}