using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Ultilities
{
    public class AuthActionFilter : FilterAttribute, IActionFilter
    {
        public int RequiredPermission { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AddHelpers.IsLogged(null) == false)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            var ui = AddHelpers.GetUserInfo(null);

            //RequiredPermission: yeu cau permission la 1
            //Neu ui.Permission < 1 thi tra ve Index, khong thi tiep tuc voi  >= 1
            if (ui.Permission < RequiredPermission)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
        }

    }
}