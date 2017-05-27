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
        static int nPerPage = 6;
        // GET: Product
        public ActionResult GetListByCategory(int? id, int page = 1)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBHEntities())
            {
                // totalP là tổng số Product
                int totalP = dc.Products.Where(p => p.CatID == id).Count();

                int nPage = totalP / nPerPage + (totalP % nPerPage > 0 ? 1 : 0);

                if (page < 1)
                {
                    page = 1;
                }
                if (page > nPage)
                {
                    page = nPage;
                }

                ViewBag.totalPage = nPage;
                ViewBag.curPage = page;

                /*
                var l = dc.Products
                    .Where(p => p.CatID == id)
                    .OrderBy(p => p.ProID)
                    .Skip((page - 1) * nPerPage)
                    .Take(nPerPage)
                    .ToList();
                return View("ListByCategory", l);

                if (l.Count() == 0)
                {
                    l = null;
                }
                return View("ListByCategory", l); */

                // Mới thêm để sửa lỗi hiển thị category không có sản phẩm bị lỗi
                ViewBag.catid = id;
                int pro1 = dc.Products.Where(n => n.CatID == id).Count();
                var l2 = dc.Products.Where(n => n.CatID == id).ToList();

                if (pro1 > 0)
                {
                    var l = dc.Products
                        .Where(p => p.CatID == id)
                        .OrderBy(p => p.ProID)
                        .Skip((page - 1) * nPerPage)
                        .Take(nPerPage)
                        .ToList();
                    return View("ListByCategory", l);
                }

                return View("ListByCategory", l2);
            }

        }
    }
}