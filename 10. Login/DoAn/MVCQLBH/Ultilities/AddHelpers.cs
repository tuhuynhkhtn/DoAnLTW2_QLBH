using MVCQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static bool IsLogged(this HtmlHelper html)
        {
            return HttpContext.Current.Session["Logged"] != null;
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
    }
}