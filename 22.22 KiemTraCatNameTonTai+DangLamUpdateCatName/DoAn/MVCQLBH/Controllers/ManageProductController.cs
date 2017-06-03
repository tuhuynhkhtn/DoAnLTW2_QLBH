using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;

namespace MVCQLBH.Controllers
{
    [AuthActionFilter(RequiredPermission = 1)]

    public class ManageProductController : Controller
    {
        // GET: ManageProduct
        public ActionResult IndexManageProduct()
        {
            return View();
        }

        // GET: ManageProduct
        public ActionResult QuanLyPro()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products.ToList();
                return View(l);
            }
        }

        // GET: ManageProduct
        public ActionResult QuanLyCat()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Categories.ToList();
                return View(l);
            }
        }

        // GET: ManageProduct
        public ActionResult QuanLyNSX()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.NhaSanXuats.ToList();
                return View(l);
            }
        }

        // GET: ManageProduct
        public ActionResult QuanLyDonHang()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Orders
                    .OrderBy(p => p.OrderDate)
                    .OrderByDescending(p => p.OrderDate)
                    .ToList();
                return View(l);
            }
        }

        // GET: ManageProduct/DeletePro
        public ActionResult DeletePro(int id)
        {
            using (var dc = new QLBHEntities())
            {
                var pD = dc.Products.Where(p => p.ProID == id).FirstOrDefault();
                if(pD != null)
                {
                    pD.BiXoa = 1;
                    dc.Products.Remove(pD);
                    dc.SaveChanges();
                }
                Ulti.DeleteProductImgs(id, Server.MapPath("~"));
                //return RedirectToAction("IndexManageProduct");
                return RedirectToAction("QuanLyPro");
            }
        }

        // GET: ManageProduct/DeletePro
        public ActionResult DeleteCat(int id)
        {
            using (var dc = new QLBHEntities())
            {
                var cD = dc.Categories.Where(c => c.CatID == id).FirstOrDefault();
                if (cD != null)
                {
                    //cD.BiXoa = 1;
                    dc.Categories.Remove(cD);
                    dc.SaveChanges();
                }
                return RedirectToAction("QuanLyCat");
            }
        }

        // GET: ManageProduct/DeletePro
        public ActionResult DeleteNSX(int id)
        {
            using (var dc = new QLBHEntities())
            {
                var nsxD = dc.NhaSanXuats.Where(nsx => nsx.IDNhaSanXuat == id).FirstOrDefault();
                if (nsxD != null)
                {
                    //cD.BiXoa = 1;
                    dc.NhaSanXuats.Remove(nsxD);
                    dc.SaveChanges();
                }
                return RedirectToAction("QuanLyNSX");
            }
        }

        // GET: ManageProduct/AddPro
        public ActionResult AddPro()
        {
            var p = new Product
            {
                ProName = "product",
                CatID = 3,
                Price = 100000,
                Quantity = 2,
                TinyDes = "tinydes..."

            };
            return View(p);
        }

        // POST: ManageProduct/AddPro
        [HttpPost]
        public ActionResult AddPro(Product p, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        {
            if(p.TinyDes == null)
            {
                p.TinyDes = string.Empty;
            }
            if (p.FullDesRaw == null)
            {
                p.FullDesRaw = string.Empty;
            }
            p.FullDes = p.FullDesRaw;
            if (p.NgayNhap == null)
            {
                p.NgayNhap = DateTime.Now.Date;
            }
            if (p.SoLuotXem == null)
            {
                p.SoLuotXem = 0;
            }
            using (var dc = new QLBHEntities())
            {
                dc.Products.Add(p);
                dc.SaveChanges();
            }
            Ulti.SaveProductImgs(p.ProID, Server.MapPath("~"), imgLg, imgSm);
            //return RedirectToAction("IndexManageProduct");
            return RedirectToAction("QuanLyPro");
        }

        // GET: ManageProduct/AddCat
        public ActionResult AddCat()
        {
            //var c = new CatInfo
            //{
            //    CatNameInfo = "category"
            //};
            //return View(c);
            return View();
        }

        // POST: ManageProduct/AddCat
        [HttpPost]
        public ActionResult AddCat(CatInfo cat)
        {
            using (var dc = new QLBHEntities())
            {
                var catTonTai = dc.Categories.Where(m => m.CatName.Contains(cat.CatNameInfo)).FirstOrDefault();
                if (catTonTai != null)
                {
                    ViewBag.ErrorMsg = "Tên loại sản phẩm đã tồn tại";
                }
                else
                {
                    var c = new Category
                    {
                        CatName = cat.CatNameInfo,
                    };

                    dc.Categories.Add(c);
                    dc.SaveChanges();
                    return RedirectToAction("QuanLyCat");
                }
            }
            //return RedirectToAction("QuanLyCat");
            return View("AddCat");
        }

        // GET: ManageProduct/AddNSX
        public ActionResult AddNSX()
        {
            //var nsx = new NSXInfo
            //{
            //    NSXNameInfo = "tennsx"
            //};
            //return View(nsx);
            return View();
        }

        // POST: ManageProduct/AddNSX
        [HttpPost]
        public ActionResult AddNSX(NSXInfo nsx)
        {
            var c = new NhaSanXuat
            {
                TenNhaSanXuat = nsx.NSXNameInfo,
            };
            using (var dc = new QLBHEntities())
            {
                dc.NhaSanXuats.Add(c);
                dc.SaveChanges();
            }
            return RedirectToAction("QuanLyNSX");
        }

        //// GET: ManageProduct/UpdateCat
        //public ActionResult UpdateCat(int id)
        //{
        //    using (var dc = new QLBHEntities())
        //    {
        //        var cU = dc.Categories.Where(c => c.CatID == id).FirstOrDefault();
        //        var cat = new CatInfo
        //        {
        //            CatIDInfo = cU.CatID,
        //            CatNameInfo = cU.CatName
        //        };
        //        return View(cat);
        //    }
        //}

        //// POST: ManageProduct/UpdateCat
        //[HttpPost]
        //public ActionResult UpdateCat(CatInfo cat)
        //{
        //    var c = new Category
        //    {
        //        CatID = cat.CatIDInfo,
        //        CatName = cat.CatNameInfo
        //    };

        //    using (var dc = new QLBHEntities())
        //    {
        //        //dc.Categories.Add(c);
        //        dc.SaveChanges();
        //    }
        //    return RedirectToAction("QuanLyCat");
        //}

        // GET: ManageProduct/UpdateCat
        public ActionResult UpdateCat(int id)
        {
            using (var dc = new QLBHEntities())
            {
                var cU = dc.Categories.Where(c => c.CatID == id).FirstOrDefault();
                var cat = new CatInfo
                {
                    CatIDInfo = cU.CatID,
                    CatNameInfo = cU.CatName
                };
                return View(cat);
            }
        }

        // POST: ManageProduct/UpdateCat
        [HttpPost]
        public ActionResult UpdateCat(CatInfo cat)
        {
            var c = new Category
            {
                CatID = cat.CatIDInfo,
                CatName = cat.CatNameInfo
            };

            using (var dc = new QLBHEntities())
            {
                //dc.Categories.Add(c);
                dc.SaveChanges();
            }
            return RedirectToAction("QuanLyCat");
        }

    }
}
