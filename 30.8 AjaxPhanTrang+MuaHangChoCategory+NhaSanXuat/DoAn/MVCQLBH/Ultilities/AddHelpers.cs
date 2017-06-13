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

        // Kiểm tra có đăng nhập chưa
        public static bool IsLogged()
        {
            if (HttpContext.Current.Session["Logged"] == null)
            {
                if (HttpContext.Current.Request.Cookies["UserId"] != null)
                {
                    int id = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value);
                    using (var dc = new QLBHEntities())
                    {
                        var user = dc.Users.Where(u => u.f_ID == id).FirstOrDefault();
                        if (user != null)
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
        //Set ProductAdmin
        public static bool ProductAdmin = true;

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
        // Kiểm tra hàng hết chưa
        public static bool IsOutOfStock1(int IDPro)
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

        public static IList<SelectListItem> GetSLILoaiInfo(this HtmlHelper html)
        {
            var l = new List<SelectListItem>();
            l.Add(new SelectListItem { Value = "1", Text = "Cao cấp" });
            l.Add(new SelectListItem { Value = "2", Text = "Trung cấp" });
            l.Add(new SelectListItem { Value = "3", Text = "Thường" });
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
                var user = dc.Users.Where(u => u.f_Username == ui.Username).FirstOrDefault();

                //Kiem tra trong database, ten username nhap vao có bị trùng với username hiện tại không
                var temp = dc.Users.Where(u => u.f_Username == Username && u.f_ID != user.f_ID).FirstOrDefault();

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

        //Add account
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
            else if (Reg.DOB != null)
            {
                DateTime dtNow = DateTime.Now;
                DateTime dt = DateTime.ParseExact(Reg.DOB, "d/M/yyyy", null);

                //Hướng dẫn sử dụng DateTime.Compare Method (DateTime, DateTime):
                //https://msdn.microsoft.com/en-us/library/system.datetime.compare(v=vs.110).aspx
                int result = DateTime.Compare(dt, dtNow);
                if (result < 0) // dt < dtNow
                {
                    //Correct
                    //is earlier than
                }
                else if (result == 0) // dt == dtNow
                {
                    //is the same time as
                    reErr.ErrorDOB = "Nhập trước ngày hiện tại";
                }
                else // dt > dtNow
                {
                    //is later than
                    reErr.ErrorDOB = "Nhập trước ngày hiện tại";
                }
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
            else if(Reg.DOB != null)
            {
                DateTime dtNow = DateTime.Now;
                DateTime dt = DateTime.ParseExact(Reg.DOB, "d/M/yyyy", null);

                int result = DateTime.Compare(dt, dtNow);
                if (result < 0)
                {
                    //Correct
                    //is earlier than
                }
                else if (result == 0)
                {
                    //is the same time as
                    reErr.ErrorDOB = "Nhập trước ngày hiện tại";
                }
                else
                {
                    //is later than
                    reErr.ErrorDOB = "Nhập trước ngày hiện tại";
                }
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

        // Kiểm tra Password mới có trùng Password cũ không
        public static bool KiemTraTrungPasswordMoi(string PasswordNew, string PasswordOld)
        {
            string passOld = Ulti.Md5Hash(PasswordOld);
            string passNew = Ulti.Md5Hash(PasswordNew);

            int result = String.Compare(passOld, passNew);

            if (result == 0)
            {
                return true;
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
                    reErr.ErrorPasswordOld = "Nhập chữ thường hoặc số, 3-18 ký tự";
                }
            }

            if (Reg.PasswordNew == null)
            {
                reErr.ErrorPasswordNew = "Nhập mật khẩu mới";
            }
            else if (Reg.PasswordNew != null)
            {
                if(KiemTraTrungPasswordMoi(Reg.PasswordNew, Reg.Password) == true)
                {
                    reErr.ErrorPasswordNew = "Mật khẩu mới không được trùng với mật khẩu hiện tại";
                }
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

        //Kiem tra sự tồn tại cua tên sản phẩm trong database
        public static bool CheckNamePro(string proName)
        {
            using(var dc = new QLBHEntities())
            {
                var product = dc.Products.Where(p => p.ProName == proName).FirstOrDefault();

                //Ton tai
                if(product != null)
                {
                    return true;
                }
            }
            return false;
        }

        //Kiem tra sự tồn tại cua tên sản phẩm trong database
        public static bool CheckNameProUpdate(int idProOld, string proNameNew)
        {
            using (var dc = new QLBHEntities())
            {
                var product = (dc.Products.Where(p => p.ProName == proNameNew)).Count();

                //Ton tai, dem dc 2 tên trung nhau
                if (product > 1)
                {
                    return true;
                }
            }
            return false;
        }

        //Product
        //Regex
        private static string rgExp = "^[^<>{}\"/|;:~!?@#$%^=&*\\]\\\\()\\[¿§«»ω⊙¤°℃℉€¥£¢¡®©+]*$";
        private static string rgExpFullDes = "^[^{}~@#$^*]*$";
        private static string rgExpXuatXu = "^[^<>{}\"/|;:,~!?@#$%^=&*\\]\\\\()\\[¿§«»ω⊙¤°℃℉€¥£¢¡®©0-9_+]*$";

        //Check Add and Update Product
        public static ProInfoError GetErrorProduct(ProInfo pro)
        {
            ProInfoError Err = new ProInfoError();

            if(pro.ProNameInfo == null)
            {
                Err.ErrorProNameInfo = "Nhập tên sản phẩm";
            }
            else if(pro.ProNameInfo != null)
            {
                if(CheckNameProUpdate(pro.ProIDInfo, pro.ProNameInfo) == true)
                {
                    Err.ErrorProNameInfo = "Đã tồn tại tên sản phẩm này";
                }
                else
                {
                    if (!Regex.IsMatch(pro.ProNameInfo, rgExp))
                    {
                        Err.ErrorProNameInfo = "Không nhập ký tự đặc biệt.";
                    }
                }
            }
            if(pro.PriceInfo == 0)
            {
                Err.ErrorPriceInfo = "Nhập giá sản phẩm";
            }

            if (pro.XuatXuInfo == null)
            {
                Err.ErrorXuatXuInfo = "Nhập xuất xứ";
            }
            else if(pro.XuatXuInfo != null)
            {
                if (!Regex.IsMatch(pro.XuatXuInfo, rgExpXuatXu))
                {
                    Err.ErrorXuatXuInfo = "Không nhập ký tự đặc biệt, số.";
                }
            }

            if(pro.NgayNhapInfo == null)
            {
                Err.ErrorNgayNhapInfo = "Nhập ngày nhập";
            }
            else if(pro.NgayNhapInfo != null)
            {
                /*https://msdn.microsoft.com/en-us/library/system.datetime.compare(v=vs.110).aspx*/

                //Neu chuoi ngay nhap vao dang: "d/m/yyyy h:m:s"
                //Thi chi lay: d/m/yyyy de paParseExact qua DateTime
                string dateIn = null;
                if (pro.NgayNhapInfo.Length > 10)
                {
                    dateIn = pro.NgayNhapInfo.Remove(10);
                }
                else
                {
                    dateIn = pro.NgayNhapInfo;
                }

                DateTime dtNow = DateTime.Now;
                DateTime dt = DateTime.ParseExact(dateIn, "d/M/yyyy", null);

                int result = DateTime.Compare(dt, dtNow);
                if (result < 0)
                {
                    //Correct
                    //is earlier than
                }
                else if (result == 0)
                {
                    //is the same time as
                    Err.ErrorNgayNhapInfo = "Nhập trước ngày hiện tại";
                }
                else
                {
                    Err.ErrorNgayNhapInfo = "Nhập trước ngày hiện tại";
                    //is later tha
                }
            }

            if(pro.TinyDesInfo == null)
            {
                Err.ErrorTinyDesInfo = "Nhập mô tả ngắn cho sản phẩm";
            }
            else if (pro.TinyDesInfo != null)
            {
                if (!Regex.IsMatch(pro.TinyDesInfo, rgExp))
                {
                    Err.ErrorTinyDesInfo = "Không nhập ký tự đặc biệt";
                }
            }
            if (pro.FullDesRaw == null)
            {
                Err.ErrorFullDesInfo = "Nhập mô tả sản phẩm";
            }
            else if (pro.FullDesRaw != null)
            {
                if (!Regex.IsMatch(pro.FullDesRaw, rgExpFullDes))
                {
                    Err.ErrorFullDesInfo = "Không nhập ký tự đặc biệt";
                }
            }

            return Err;
        }
    }
}