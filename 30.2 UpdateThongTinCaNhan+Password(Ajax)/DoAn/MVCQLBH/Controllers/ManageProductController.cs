using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;
using System.Data.Entity;

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
            var p = new ProInfo
            {
                ProNameInfo = "product",
                CatIDInfo = 3,
                PriceInfo = 100000,
                QuantityInfo = 2,
                XuatXuInfo = "Việt Nam",
                TinyDesInfo = "tinydes..."

            };
            return View(p);
            //return View();
        }

        //// POST: ManageProduct/AddPro
        //[HttpPost]
        //public ActionResult AddPro(Product p, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        //{
        //    if(p.TinyDes == null)
        //    {
        //        p.TinyDes = string.Empty;
        //    }
        //    if (p.FullDesRaw == null)
        //    {
        //        p.FullDesRaw = string.Empty;
        //    }
        //    p.FullDes = p.FullDesRaw;
        //    if (p.NgayNhap == null)
        //    {
        //        p.NgayNhap = DateTime.Now.Date;
        //    }
        //    if (p.SoLuotXem == null)
        //    {
        //        p.SoLuotXem = 0;
        //    }
        //    using (var dc = new QLBHEntities())
        //    {
        //        dc.Products.Add(p);
        //        dc.SaveChanges();
        //    }
        //    Ulti.SaveProductImgs(p.ProID, Server.MapPath("~"), imgLg, imgSm);
        //    //return RedirectToAction("IndexManageProduct");
        //    return RedirectToAction("QuanLyPro");
        //}

        // POST: ManageProduct/AddPro
        [HttpPost]
        public ActionResult AddPro(ProInfo pro, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        {
            if (pro.TinyDesInfo == null)
            {
                pro.TinyDesInfo = string.Empty;
            }
            if (pro.FullDesRaw == null)
            {
                pro.FullDesRaw = string.Empty;
            }
            //p.FullDes = p.FullDesRaw;
            if (pro.NgayNhapInfo == null)
            {
                pro.NgayNhapInfo = DateTime.Now.Date;
            }
            if (pro.SoLuotXemInfo == null)
            {
                pro.SoLuotXemInfo = 0;
            }

            using (var dc = new QLBHEntities())
            {
                var proTonTai = dc.Products.Where(m => m.ProName == pro.ProNameInfo).FirstOrDefault();
                if (proTonTai != null)
                {
                    ViewBag.ErrorMsg = "Tên sản phẩm đã tồn tại";
                }
                else
                {
                    var p = new Product
                    {
                        ProName = pro.ProNameInfo,
                        TinyDes = pro.TinyDesInfo,
                        FullDes = pro.FullDesRaw,
                        Price = pro.PriceInfo,
                        CatID = pro.CatIDInfo,
                        Quantity = pro.QuantityInfo,
                        NgayNhap = DateTime.Now.Date,
                        SoLuotXem = 0,
                        XuatXu = pro.XuatXuInfo,
                        Loai = pro.LoaiInfo,
                        IDNhaSanXuat = pro.IDNhaSanXuatInfo,
                        BiXoa = 0,
                        SoLuongDaBan = pro.SoLuongDaBanInfo
                    };

                    dc.Products.Add(p);
                    dc.SaveChanges();
                    Ulti.SaveProductImgs(p.ProID, Server.MapPath("~"), imgLg, imgSm);
                    return RedirectToAction("QuanLyPro");
                }
                //Ulti.SaveProductImgs(pro.ProIDInfo, Server.MapPath("~"), imgLg, imgSm);
                //return RedirectToAction("IndexManageProduct");
                return RedirectToAction("AddPro");
            }

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
                var catTonTai = dc.Categories.Where(m => m.CatName == cat.CatNameInfo).FirstOrDefault();
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
            using (var dc = new QLBHEntities())
            {
                var nsxTonTai = dc.NhaSanXuats.Where(m => m.TenNhaSanXuat == nsx.NSXNameInfo).FirstOrDefault();
                if (nsxTonTai != null)
                {
                    ViewBag.ErrorMsg = "Tên nhà sản xuất đã tồn tại";
                }
                else
                {
                    var c = new NhaSanXuat
                    {
                        TenNhaSanXuat = nsx.NSXNameInfo,
                    };

                    dc.NhaSanXuats.Add(c);
                    dc.SaveChanges();
                    return RedirectToAction("QuanLyNSX");
                }
            }
            return View("AddNSX");
        }

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

        // POST: ManageProduct/UpdateInfoCat
        [HttpPost]
        public ActionResult UpdateInfoCat(CatInfo cat)
        {
            using (var dc = new QLBHEntities())
            {
                // Kiểm tra ID loại sản phẩm nhập vào có tồn tại không
                var cU = dc.Categories.Where(c => c.CatID == cat.CatIDInfo).FirstOrDefault();
                if (cU != null)
                {
                    cU.CatName = cat.CatNameInfo;

                    // Kiểm tra tên loại sản phẩm nhập vào có bị trùng không
                    var a = dc.Categories.Where(c => c.CatName == cU.CatName).FirstOrDefault();
                    if(a != null)
                    {
                        ViewBag.ErrorMsg = "Tên loại sản phẩm đã tồn tại";
                    }
                    else
                    {
                        dc.Entry(cU).State = EntityState.Modified;
                        dc.SaveChanges();
                        return RedirectToAction("QuanLyCat");
                    }
                }
               
            }
            return View("UpdateCat");
        }

        // GET: ManageProduct/UpdateNSX
        public ActionResult UpdateNSX(int id)
        {
            using (var dc = new QLBHEntities())
            {
                var nsxU = dc.NhaSanXuats.Where(c => c.IDNhaSanXuat == id).FirstOrDefault();
                var nsx = new NSXInfo
                {
                    NSXIDInfo = nsxU.IDNhaSanXuat,
                    NSXNameInfo = nsxU.TenNhaSanXuat
                };
                return View(nsx);
            }
        }

        // POST: ManageProduct/UpdateInfoNSX
        [HttpPost]
        public ActionResult UpdateInfoNSX(NSXInfo nsx)
        {
            using (var dc = new QLBHEntities())
            {
                var nsxU = dc.NhaSanXuats.Where(c => c.IDNhaSanXuat == nsx.NSXIDInfo).FirstOrDefault();
                if (nsxU != null)
                {
                    nsxU.TenNhaSanXuat = nsx.NSXNameInfo;

                    var a = dc.NhaSanXuats.Where(c => c.TenNhaSanXuat == nsxU.TenNhaSanXuat).FirstOrDefault();
                    if (a != null)
                    {
                        ViewBag.ErrorMsg = "Tên nhà sản xuất đã tồn tại";
                    }
                    else
                    {
                        dc.Entry(nsxU).State = EntityState.Modified;
                        dc.SaveChanges();
                        return RedirectToAction("QuanLyNSX");
                    }
                }
            }
            return View("UpdateNSX");
        }

        //POST: ManageProduct/UpdateOrder
        [HttpPost]
        public ActionResult UpdateOrder(int orderId, int status)
        {
            //Status = 0 - chua giao, cap nhat thanh da giao -1
            //Status = 1 - da giao, cap nhat chua giao - 0
            using (var dc = new QLBHEntities())
            {
                var order = dc.Orders.Where(o => o.OrderID == orderId).FirstOrDefault();
                if(status == 0)
                {
                    order.TinhTrangGiao = 1;
                }
                else
                {
                    order.TinhTrangGiao = 0;
                }
                
                dc.SaveChanges();
                return RedirectToAction("QuanLyDonHang", "ManageProduct");
            }
        }
    }
}
