using MVCQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Ultilities
{
    public static class AddHelpers
    {
        public static object HtttpContext { get; private set; }

        public static MvcHtmlString LessString(this HtmlHelper html, string str, int maxLength)
        {
            if(str.Length < maxLength)
            {
                return new MvcHtmlString(str);
            }
            return new MvcHtmlString(
                string.Format("{0}...", str.Substring(0, maxLength - 3)));
        }

        public static string Price2String(this HtmlHelper html, decimal price)
        {
            return string.Format("{0:N0} đ", price);
        }

        // Kiểm tra có đăng nhập chưa
        public static bool IsLogged(this HtmlHelper html)
        {
            if(HttpContext.Current.Session["Logged"] == null)
            {
                if(HttpContext.Current.Request.Cookies["UserId"] != null)
                {
                    int id = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value);
                    using (var dc = new QLBHEntities())
                    {
                        var user = dc.Users.Where(u => u.f_ID == id).FirstOrDefault();
                        if(user != null)
                        {
                            HttpContext.Current.Session["Logged"] = new UserInfo { Username = user.f_Username, Permission = user.f_Permission };
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }

        // Kiểm tra có phải là Admin đăng nhập không
        public static bool IsLoggedAdmin(this HtmlHelper html)
        {
            if (HttpContext.Current.Request.Cookies["UserId"] != null)
            {
                int id = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value);
                using (var dc = new QLBHEntities())
                {
                    var user = dc.Users.Where(u => u.f_ID == id && u.f_Permission == 1).FirstOrDefault();
                    if (user != null)
                    {
                        HttpContext.Current.Session["Logged"] = new UserInfo { Username = user.f_Username, Permission = user.f_Permission };
                        return true;
                    }
                }
            }
            return false;
        }

        // Kiểm tra hàng hết chưa
        public static bool IsOutOfStock(this HtmlHelper html, int IDPro)
        {
            //int id = int.Parse(HttpContext.Current.Request.Cookies["IDPro"].Value);
            using (var dc = new QLBHEntities())
            {
                var product = dc.Products.Where(u => u.ProID == IDPro && u.Quantity == 0).FirstOrDefault();
                if (product != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetUsername(this HtmlHelper html)
        {
            var ui = HttpContext.Current.Session["Logged"] as UserInfo;
            if(ui != null)
            {
                return ui.Username;
            }
            return "";
        }

        public static UserInfo GetUserInfo(this HtmlHelper html)
        {
            var ui = HttpContext.Current.Session["Logged"] as UserInfo;
            return ui;
        }

        public static Cart GetCart(this HtmlHelper html)
        {
            if(HttpContext.Current.Session["cart"] == null)
            {
                HttpContext.Current.Session["cart"] = new Cart();
            }
            return (Cart)HttpContext.Current.Session["cart"];
        }

        public static IList<SelectListItem> GetSLICat(this HtmlHelper html)
        {
            var l = new List<SelectListItem>();
            using (var dc = new QLBHEntities())
            {
                foreach(var c in dc.Categories.ToList())
                {
                    l.Add(new SelectListItem
                    {
                        Value = c.CatID.ToString(),
                        Text = c.CatName
                    });
                }
            }
            return l;
        }

        public static IList<SelectListItem> GetSLINhaSanXuat(this HtmlHelper html)
        {
            var l = new List<SelectListItem>();
            using (var dc = new QLBHEntities())
            {
                foreach (var c in dc.NhaSanXuats.ToList())
                {
                    l.Add(new SelectListItem
                    {
                        Value = c.IDNhaSanXuat.ToString(),
                        Text = c.TenNhaSanXuat
                    });
                }
            }
            return l;
        }

        //Registing
        public static bool CheckEmpty(UserRegisting Reg)
        {
            if (Reg.Username == null || Reg.Password == null || Reg.PasswordRetype == null
                || Reg.Name == null || Reg.Email == null || Reg.DOB == null || Reg.CaptchaCode == null)
            {
                return true;
            }
            return false;
        }

        //Check username exist
        public static bool XacThucUsername(string Username)
        {
            using (var dc = new QLBHEntities())
            {
                //Username nhập vào
                var _username = dc.Users.Where(u => u.f_Username == Username).FirstOrDefault();

                //Username dang cap nhat
                var ui = HttpContext.Current.Session["Logged"] as UserInfo;

                //Kiem tra trong database, ten username nhap vao có bị trùng với username hiện tại không
                var temp = dc.Users.Where(u => u.f_Username == Username && u.f_ID != ui.UserID).FirstOrDefault();

                if(temp != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Check username exist
        public static bool XacThucUsernameRegisting(string Username)
        {
            using (var dc = new QLBHEntities())
            {
                //Username nhập vào
                var _username = dc.Users.Where(u => u.f_Username == Username).FirstOrDefault();

                if (_username != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Regular
        private static string rgUsername = @"^[a-z0-9_-]{3,16}$";
        private static string rgPassword = @"^[a-zA-Z0-9_-]{3,18}$";
        private static string rgName = @"^[a-zA-Z -]{3,30}$";

        public static RegisterError GetErrorNullRegisting(UserRegisting Reg)
        {
            //Empty
            RegisterError reErr = new RegisterError();
            if (Reg.Username == null)
            {
                reErr.ErrorUsername = "Nhập Username";
            }
            else if (Reg.Username != null)
            {
                bool b = XacThucUsernameRegisting(Reg.Username);
                if (b == true)
                {
                    reErr.ErrorUsername = "Đã tồn tại Username";
                }
                else if (!Regex.IsMatch(Reg.Username, rgUsername))
                {
                    reErr.ErrorUsername = "Nhập chữ thường hoặc số, 3-16 ký tự";
                }
            }

            if (Reg.Password == null)
            {
                reErr.ErrorPassword = "Nhập mật khẩu";
            }
            else if (Reg.PasswordRetype != null)
            {
                if (!Regex.IsMatch(Reg.Password, rgPassword))
                {
                    reErr.ErrorPassword = "Nhập chữ hoặc số, 3-18 ký tự";
                }
            }

            if (Reg.PasswordRetype == null)
            {
                reErr.ErrorPasswordRetype = "Nhập lại mật khẩu";
            }
            else if (Reg.PasswordRetype != null)
            {
                if (Reg.PasswordRetype != Reg.Password)
                {
                    reErr.ErrorPasswordRetype = "Nhập lại không đúng";
                }
            }

            if (Reg.Name == null)
            {
                reErr.ErrorName = "Nhập tên";
            }
            else if (Reg.Name != null)
            {
                if (!Regex.IsMatch(Reg.Name, rgName))
                {
                    reErr.ErrorName = "Nhập chữ không dấu, 3-30 ký tự";
                }
            }

            if (Reg.Email == null)
            {
                reErr.ErrorEmail = "Nhập email";
            }

            if (Reg.DOB == null)
            {
                reErr.ErrorDOB = "Nhập ngày sinh";
            }

            if (Reg.CaptchaCode == null)
            {
                reErr.ErrorCaptcha = "Nhập captcha";
            }
            return reErr;
        }

        public static ProfileError GetErrorNullProfile(UserInfo Reg)
        {
            //Empty
            ProfileError reErr = new ProfileError();
            if (Reg.Username == null)
            {
                reErr.ErrorUsername = "Nhập Username";
            }
            else if (Reg.Username != null)
            {
                bool b = XacThucUsername(Reg.Username);
                if (b == true)
                {
                    reErr.ErrorUsername = "Đã tồn tại Username";
                }
                else if (!Regex.IsMatch(Reg.Username, rgUsername))
                {
                    reErr.ErrorUsername = "Nhập chữ thường hoặc số, 3-16 ký tự";
                }
            }

            if (Reg.Name == null)
            {
                reErr.ErrorName = "Nhập tên";
            }
            else if (Reg.Name != null)
            {
                if (!Regex.IsMatch(Reg.Name, rgName))
                {
                    reErr.ErrorName = "Nhập chữ không dấu, 3-30 ký tự";
                }
            }

            if (Reg.Email == null)
            {
                reErr.ErrorEmail = "Nhập email";
            }

            if (Reg.DOB == null)
            {
                reErr.ErrorDOB = "Nhập ngày sinh";
            }

            return reErr;
        }

        // Xác thực Password
        public static bool XacThucPassword(int UserID, string Password)
        {
            string pass = Ulti.Md5Hash(Password);

            using (var dc = new QLBHEntities())
            {
                var _password = dc.Users.Where(u => u.f_ID == UserID && u.f_Password == pass).FirstOrDefault();

                //Dung
                if (_password != null)
                {
                    return true;
                }
            }
            return false;
        }

        public static PasswordError GetErrorNullPassword(UserInfo Reg)
        {
            //Empty
            PasswordError reErr = new PasswordError();

            if (Reg.Password == null)
            {
                reErr.ErrorPasswordOld = "Nhập mật khẩu hiện tại";
            }
            else if (Reg.Password != null)
            {
                bool b = XacThucPassword(Reg.UserID, Reg.Password);
                if (b == false)
                {
                    reErr.ErrorPasswordOld = "Mật khẩu hiện tại không trùng khớp";
                }
                if (!Regex.IsMatch(Reg.Password, rgPassword))
                {
                    reErr.ErrorPasswordOld = "Nhập chữ thường hoặc số, 3-16 ký tự";
                }
            }

            if (Reg.PasswordNew == null)
            {
                reErr.ErrorPasswordNew = "Nhập mật khẩu mới";
            }
            else if (Reg.PasswordNew != null)
            {
                if (!Regex.IsMatch(Reg.PasswordNew, rgPassword))
                {
                    reErr.ErrorPasswordNew = "Nhập chữ hoặc số, 3-18 ký tự";
                }
            }

            if (Reg.PasswordRetype == null)
            {
                reErr.ErrorPasswordRetype = "Nhập lại mật khẩu";
            }
            else if (Reg.PasswordRetype != null)
            {
                if (Reg.PasswordRetype != Reg.PasswordNew)
                {
                    reErr.ErrorPasswordRetype = "Nhập lại không đúng";
                }
            }

            return reErr;
        }



    }
}