using MVCQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // POST: Cart/Add
        [HttpPost]
        public ActionResult Add(int proId, int quantity)
        {
            if(Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            using (var dc = new QLBHEntities())
            {
                var pro = dc.Products.Where(p => p.ProID == proId).FirstOrDefault();
                if(pro != null)
                {
                    c.Items.Add(new CartItem { Product = pro, Quantity = quantity });
                }
            }
            return RedirectToAction("Detail", "Product", new { id = proId });
        }
    }
}