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
        private static int errQ = -1;
        private static int quantityIn = 0;
        private static string proName = "";
        // GET: Cart
        public ActionResult DetailCart()
        {
            ViewBag.ErrorQ = errQ;
            ViewBag.proName = proName;
            ViewBag.QuantityIn =  quantityIn;
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


            using (var dc = new QLBHEntities())
            {
                var pro = dc.Products.Where(p => p.ProID == proId).FirstOrDefault();
                if (pro.Quantity < quantity)
                {
                    return RedirectToAction("DetailPro", "Product", new { id = proId, errorCart = 0 });
                }
            }

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

        //Cat
        [HttpPost]
        public ActionResult AddCIFromListProductAjax(int proId, int catId, int page)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            return RedirectToAction("GetListByCategory", "Product", new { id = catId, page = page });
        }

        //NSX
        [HttpPost]
        public ActionResult AddCIFromListProductNSXAjax(int proId, int nsxId, int page)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            return RedirectToAction("GetListByNSX", "Product", new { id = nsxId, page = page });
        }

        // POST: Cart/AddNSXIFromListProduct
        [HttpPost]
        public ActionResult AddNSXIFromListProduct(int proId, int nsxId, int page)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            return RedirectToAction("GetListByNSX", "Product", new { id = nsxId, page = page });
        }

        // POST: Cart/AddProIFromListProduct
        [HttpPost]
        public ActionResult AddProIFromListProduct(int proId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            return RedirectToAction("Index", "Home");
        }

        //Them san pham tu danh sach cung loai, cung nha san xuat
        //Them xong tra ve trang detail cua san pham dang xem
        [HttpPost]
        public ActionResult AddProIFromListDetail(int proIdAddCart, int proIdDetail)
        {
            //if (!id.HasValue)
            //{
            //    return RedirectToAction("Action", "Home");
            //}
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proIdAddCart);
            return RedirectToAction("DetailPro", "Product", new { id = proIdDetail });
        }

        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int proId)
        {
            var c = Session["cart"] as Cart;
            c.Remove(proId);
            return RedirectToAction("DetailCart", "cart");
        }

        // POST: Cart/Update
        [HttpPost]
        public ActionResult Update(int proId, int quantity)
        {
            var c = Session["cart"] as Cart;

            int error;

            //c.Update(proId, quantity);
            c.Update(proId, quantity, out error);

            if(error == 1)
            {
                errQ = 1;
            }
            else
            {
                errQ = 0;
            }

            using (var dc = new QLBHEntities())
            {
                var pro = dc.Products.Where(p => p.ProID == proId).FirstOrDefault();
                proName = pro.ProName;
            }

            quantityIn = quantity;
            return RedirectToAction("DetailCart", "cart");
        }

        // POST: Cart/Update
        [HttpPost]
        public ActionResult Checkout()
        {
            var c = Session["cart"] as Cart;
            var ui = Session["Logged"] as UserInfo;

            //Kiem tra so luong cua Gio hang voi so luong con lai trong kho
            using (var dc = new QLBHEntities())
            {

                foreach (var ci in c.Items)
                {
                    var proData = dc.Products.Where(i => i.ProID == ci.Product.ProID).FirstOrDefault();

                    if (proData.Quantity < ci.Quantity)
                    {
                        //Day ra ViewBag
                        errQ = 1;
                        proName = proData.ProName;
                        quantityIn = ci.Quantity;

                        return RedirectToAction("DetailCart", "cart");
                    }
                }
            }
            //Ket thuc kiem tra
            using (var dc = new QLBHEntities())
            {
                var user = dc.Users.Where(u => u.f_Username == ui.Username).FirstOrDefault();
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    User = user,
                };
                dc.Orders.Add(order);

                decimal amount = 0, total = 0;

                foreach(var ci in c.Items)
                {
                    var p = dc.Products.Where(i => i.ProID == ci.Product.ProID).FirstOrDefault();

                    // Cập nhật số lượng bán, số lượng tồn
                    p.Quantity -= 1;
                    p.SoLuongDaBan += 1;
                    dc.SaveChanges();

                    amount = p.Price * ci.Quantity;
                    total += amount;

                    var od = new OrderDetail
                    {
                        Order = order,
                        Product = p,
                        Quantity = ci.Quantity,
                        Price = p.Price,
                        Amount = amount
                    };
                    dc.OrderDetails.Add(od);
                }
                order.Total = total;
                order.TinhTrangGiao = 0;
                dc.SaveChanges();
            }

                c.Checkout();
            return RedirectToAction("DetailCart", "cart");
        }

        //[HttpPost]
        //public ActionResult AddProIFromListSearch(int proId, string noidungSearch, int page)
        //{
        //    if (Session["cart"] == null)
        //    {
        //        Session["cart"] = new Cart();
        //    }
        //    var c = Session["cart"] as Cart;
        //    c.AddItem(proId);
        //    //return RedirectToAction("TimKiem", "Product", noidungSearch, page);
        //    return RedirectToAction("TimKiem", "Product", new { noidungSearch = noidungSearch, page = page });
        //}

        [HttpPost]
        public ActionResult AddProIFromListSearch(int proId, string ndSearch, int page)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
            c.AddItem(proId);
            //return RedirectToAction("TimKiem", "Product", noidungSearch, page);
            return RedirectToAction("TimKiem", "Product", new { noidungSearch = ndSearch, page = page });
        }
    }
}