using MVCQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult GetListByCategory(int catId)
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.CatID == catId)
                    .ToList();
                return View("ListByCategory", l);
            }

        }
    }
}