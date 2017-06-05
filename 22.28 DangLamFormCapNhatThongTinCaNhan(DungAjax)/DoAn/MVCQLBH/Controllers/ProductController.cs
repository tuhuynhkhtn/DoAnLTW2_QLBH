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
        static int nPerPage = 6; // Số lượng sản phẩm tối đa hiển thị trên 1 trang
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

                if (totalP == 0)
                {
                    return View("ListByCategory", new List<Product>());
                }

                // Tính tổng số trang phải hiển thị
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
                ViewBag.cId = id;

                var l = dc.Products
                    .Where(p => p.CatID == id)
                    .OrderBy(p => p.ProID)
                    .Skip((page - 1) * nPerPage)
                    .Take(nPerPage)
                    .ToList();
                return View("ListByCategory", l);
            }
        }

        public ActionResult AjaxListByCat(int? id, int page = 1)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBHEntities())
            {
                // totalP là tổng số Product
                int totalP = dc.Products.Where(p => p.CatID == id).Count();

                if (totalP == 0)
                {
                    return View("AjaxListByCat", new List<Product>());
                }

                // Tính tổng số trang phải hiển thị
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
                ViewBag.cId = id;

                var l = dc.Products
                    .Where(p => p.CatID == id)
                    .OrderBy(p => p.ProID)
                    .Skip((page - 1) * nPerPage)
                    .Take(nPerPage)
                    .ToList();
                return View("AjaxListByCat", l);
            }
        }

        public ActionResult GetListByNSX(int? id, int page = 1)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBHEntities())
            {
                // totalP là tổng số Product
                int totalP = dc.NhaSanXuats.Where(p => p.IDNhaSanXuat == id).Count();

                if (totalP == 0)
                {
                    return View("ListByCategory", new List<Product>());
                }

                // Tính tổng số trang phải hiển thị
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
                ViewBag.nsxId = id;

                var l = dc.Products
                    .Where(p => p.IDNhaSanXuat == id)
                    .OrderBy(p => p.ProID)
                    .Skip((page - 1) * nPerPage)
                    .Take(nPerPage)
                    .ToList();
                return View("ListByNSX", l);
            }
        }

        public ActionResult DetailPro(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            using (var dc = new QLBHEntities())
            {
                var product = dc.Products.Where(p => p.ProID == id).FirstOrDefault();

                product.SoLuotXem += 1;
                dc.SaveChanges(); // Phải lưu Database lại thì SoLuotXem mới cộng 1 được

                Session["IDPro"] = product.ProID;

                //var product = from p in dc.Products
                //              from n in dc.NhaSanXuats
                //              where p.IDNhaSanXuat == n.IDNhaSanXuat
                //              select new
                //              {
                //                  GiaBan = p.Price,
                //                  SoLuotXem = p.SoLuotXem,
                //                  SoLuongBan = p.Quantity,
                //                  MoTa = p.FullDes,
                //                  XuatXu = p.XuatXu,
                //                  Loai = p.Loai,
                //                  TenNhaSanXuat = n.TenNhaSanXuat
                //              };

                string ten = (from p in dc.Products
                              from n in dc.NhaSanXuats
                              where p.IDNhaSanXuat == n.IDNhaSanXuat && p.ProID == id
                              select n.TenNhaSanXuat).FirstOrDefault().ToString();
                ViewBag.TenNSX = ten;

                return View(product);
            }
        }

        // GET: Product
        public ActionResult HienThi5SPCungLoai(string loaiSP, int idPro)
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.BiXoa == 0 && p.Loai == loaiSP && p.ProID != idPro)
                    .Take(5)
                    .ToList();
                return PartialView("_PartialHienThi5SP", l);
            }
        }

        // GET: Product
        public ActionResult HienThi5SPCungNSX(int nsx, int idPro)
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.BiXoa == 0 && p.IDNhaSanXuat == nsx && p.ProID != idPro)
                    .Take(5)
                    .ToList();
                return PartialView("_PartialHienThi5SP", l);
            }
        }

        // GET: Product
        public ActionResult TimKiem(string noidungSearch, int page = 1)
        {
            ViewBag.KeySearch = noidungSearch;
            using (var dc = new QLBHEntities())
            {
                // totalP là tổng số Product
                //int totalP = (from p in dc.Products
                //              from n in dc.NhaSanXuats
                //              from c in dc.Categories
                //              where p.IDNhaSanXuat == n.IDNhaSanXuat && p.CatID == c.CatID
                //                && (p.ProName == noidungSearch || c.CatName == noidungSearch || n.TenNhaSanXuat == noidungSearch)
                //              select p).Count();

                //totalP là tổng số Product
                int price = 0;

                if (!int.TryParse(noidungSearch, out price))
                {
                    price = 0;
                }
                
                //int totalP = dc.Products.Where(p => p.ProName.Contains(noidungSearch) || p.Price == price).Count();

                int totalP1 = (from p in dc.Products
                               from n in dc.NhaSanXuats
                               from c in dc.Categories
                               where p.IDNhaSanXuat == n.IDNhaSanXuat && p.CatID == c.CatID
                                 && (p.ProName.Contains(noidungSearch) || c.CatName.Contains(noidungSearch) || n.TenNhaSanXuat.Contains(noidungSearch) || p.Price == price)
                               select p).Count();


                if (totalP1 == 0)
                {
                    return View("ListTimKiem", new List<Product>());
                }

                // Tính tổng số trang phải hiển thị
                int nPage = totalP1 / nPerPage + (totalP1 % nPerPage > 0 ? 1 : 0);

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

                //var l = (from p in dc.Products
                //         from n in dc.NhaSanXuats
                //         from c in dc.Categories
                //         where p.IDNhaSanXuat == n.IDNhaSanXuat && p.CatID == c.CatID
                //             && (p.ProName == noidungSearch || c.CatName == noidungSearch || n.TenNhaSanXuat == noidungSearch)
                //         select p).OrderBy(p => p.ProID).Skip((page - 1) * nPerPage).Take(nPerPage).ToList();
                //var l = dc.Products.Where(p => p.ProName.Contains(noidungSearch) || p.Price == price).ToList();

                var l = (from p in dc.Products
                               from n in dc.NhaSanXuats
                               from c in dc.Categories
                               where p.IDNhaSanXuat == n.IDNhaSanXuat && p.CatID == c.CatID
                                 && (p.ProName.Contains(noidungSearch) || c.CatName.Contains(noidungSearch) || n.TenNhaSanXuat.Contains(noidungSearch) || p.Price == price)
                               select p)
                               .OrderBy(p => p.ProID)
                               .Skip((page - 1) * nPerPage)
                               .Take(nPerPage).ToList();

                return View("ListTimKiem", l);
            }
        }

    }
}