using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Ultilities
{
    public class AuthActionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {   
            if(HttpContext.Current.Session["Logged"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}