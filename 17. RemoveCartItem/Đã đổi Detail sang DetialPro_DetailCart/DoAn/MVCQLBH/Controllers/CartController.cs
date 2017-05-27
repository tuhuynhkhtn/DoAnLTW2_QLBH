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
        public ActionResult DetailCart()
        {
            var cart = Session["cart"] as Cart;
            return View(cart);
        }

        // POST: Cart/AddCIFomDetail
        [HttpPost]
        public ActionResult AddCIFomDetail(int proId, int quantity)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId, quantity);

            //using (var dc = new QLBHEntities())
            //{
            //    var pro = dc.Products.Where(p => p.ProID == proId).FirstOrDefault();
            //    if(pro != null)
            //    {
            //        c.Items.Add(new CartItem { Product = pro, Quantity = quantity });
            //    }
            //}

            return RedirectToAction("DetailPro", "Product", new { id = proId });
        }

        // POST: Cart/AddCIFromListProduct
        [HttpPost]
        public ActionResult AddCIFromListProduct(int proId, int catId, int page)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            return RedirectToAction("GetListByCategory", "Product", new { id = catId, page = page });
        }

        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int proId)
        {
            var c = Session["cart"] as Cart;
            c.Remove(proId);
            return RedirectToAction("DetailCart", "cart");
        }

    }
}