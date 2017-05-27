using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Ultilities
{
    public static class AddHelpers
    {
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
    }
}