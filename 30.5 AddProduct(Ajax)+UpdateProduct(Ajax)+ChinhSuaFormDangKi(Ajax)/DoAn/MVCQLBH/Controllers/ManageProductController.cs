using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCQLBH.Models;
using MVCQLBH.Ultilities;
using System.Data.Entity;
using System.Threading;
using System.IO;
using System.Drawing;

namespace MVCQLBH.Controllers
{
    [AuthActionFilter(RequiredPermission = 1)]

    public class ManageProductController : Controller
    {
        static int nPerPage = 6;
        // GET: ManageProduct
        public ActionResult IndexManageProduct()
        {
            return View();
        }

        // GET: ManageProduct
        public ActionResult QuanLyPro(int page = 1)
        {
            using (var dc = new QLBHEntities())
            {
                // totalP là tổng số Product
                int totalP = dc.Products.Count();

                if (totalP == 0)
                {
                    return View("AjaxListByCat", new List<Product>());
                }

                // Tính tổng số trang phải hiển thị
                int nPage = totalP / nPerPage + (totalP % nPerPage > 0 ? 1 : 0);

                ViewBag.totalPage = nPage;
                ViewBag.curPage = page;
                var l = dc.Products
                        .OrderBy(p => p.ProID)
                        .Skip((page - 1) * nPerPage)
                        .Take(nPerPage)
                        .ToList();
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
        public ActionResult DeletePro(int pId)
        {
            using (var dc = new QLBHEntities())
            {
                var pD = dc.Products.Where(p => p.ProID == pId).FirstOrDefault();
                if(pD != null)
                {
                    pD.BiXoa = 1;
                    dc.Products.Remove(pD);
                    dc.SaveChanges();
                }
                Ulti.DeleteProductImgs(pId, Server.MapPath("~"));
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

        // POST: ManageProduct/AddPro
        [HttpPost]
        public ActionResult AddPro(ProInfo pro, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        {
            if (pro.FullDesRaw == null)
            {
                pro.FullDesRaw = string.Empty;
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
                        NgayNhap = DateTime.ParseExact("18/03/2017", "dd/MM/yyyy", null),
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

        // GET: ManageProduct/AjaxAddPro
        public ActionResult AjaxAddPro()
        {
            //var p = new ProInfo
            //{
            //    ProNameInfo = "product",
            //    CatIDInfo = 3,
            //    PriceInfo = 100000,
            //    QuantityInfo = 2,
            //    XuatXuInfo = "Việt Nam",
            //    TinyDesInfo = "tinydes..."

            //};
            //return View(p);
            return View();
        }

        // POST: ManageProduct/AddPro
        //Khong submit dc file bang Ajax
        [HttpPost]
        public ActionResult AjaxAddPro(ProInfo proInfo, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        {
            ProInfoError ErrP = new ProInfoError();
            ErrP = AddHelpers.GetErrorProduct(proInfo);

            if(imgLg == null)
            {
                ErrP.ErrorImgLgInfo = "Chọn hình lớn";
            }
            if(imgSm == null)
            {
                ErrP.ErrorImgSmInfo = "Chọn hình phụ";
            }

            if(ErrP.ErrorProNameInfo != null || ErrP.ErrorXuatXuInfo != null || ErrP.ErrorNgayNhapInfo != null || ErrP.ErrorTinyDesInfo != null || ErrP.ErrorImgLgInfo != null || ErrP.ErrorImgSmInfo != null || ErrP.ErrorFullDesInfo != null)
            {
                Thread.Sleep(2000);
                return Json(ErrP);
            }
            else
            {
                //Add Product
                using (var dc = new QLBHEntities())
                {
                    DateTime dt = DateTime.ParseExact(proInfo.NgayNhapInfo, "d/M/yyyy", null);
                    var p = new Product
                    {
                        ProName = proInfo.ProNameInfo,
                        TinyDes = proInfo.TinyDesInfo,
                        FullDes = proInfo.FullDesRaw,
                        Price = proInfo.PriceInfo,
                        CatID = proInfo.CatIDInfo,
                        Quantity = proInfo.QuantityInfo,
                        NgayNhap = dt,
                        SoLuotXem = 0,
                        XuatXu = proInfo.XuatXuInfo,
                        Loai = proInfo.LoaiInfo,
                        IDNhaSanXuat = proInfo.IDNhaSanXuatInfo,
                        BiXoa = 0,
                        SoLuongDaBan = proInfo.SoLuongDaBanInfo
                    };

                    dc.Products.Add(p);
                    dc.SaveChanges();
                    Ulti.SaveProductImgs(p.ProID, Server.MapPath("~"), imgLg, imgSm);
                    Thread.Sleep(2000);
                    return Json(ErrP);
                }
            }
        }

        //GET: Manageproduct/UpdatePro
        public ActionResult UpdatePro(int pIdUpdate)
        {
            using (var dc = new QLBHEntities())
            {
                var product = dc.Products.Where(p => p.ProID == pIdUpdate).FirstOrDefault();

                ProInfo proInfo = new ProInfo();

                proInfo.ProIDInfo = product.ProID;
                proInfo.ProNameInfo = product.ProName;
                proInfo.TinyDesInfo = product.TinyDes;
                proInfo.FullDesRaw = product.FullDesRaw;
                proInfo.PriceInfo = product.Price;
                proInfo.CatIDInfo = product.CatID;
                proInfo.QuantityInfo = product.Quantity;
                proInfo.NgayNhapInfo = product.NgayNhap.ToString();
                proInfo.SoLuotXemInfo = (int)product.SoLuotXem;
                proInfo.XuatXuInfo = product.XuatXu;
                proInfo.LoaiInfo = (int)product.Loai;
                proInfo.IDNhaSanXuatInfo = (int)product.IDNhaSanXuat;
                proInfo.BiXoaInfo = (byte)product.BiXoa;
                proInfo.SoLuongDaBanInfo = (int)product.SoLuongDaBan;

                return View(proInfo);
            }
        }

        ////GET: Manageproduct/AjaxUpdatePro
        public ActionResult AjaxUpdatePro(int pIdUpdate)
        {
            using (var dc = new QLBHEntities())
            {
                var product = dc.Products.Where(p => p.ProID == pIdUpdate).FirstOrDefault();
                product.FullDes.GetTypeCode();

                string fullDes = product.FullDes;

                ProInfo proInfo = new ProInfo();

                proInfo.ProIDInfo = product.ProID;
                proInfo.ProNameInfo = product.ProName;
                proInfo.TinyDesInfo = product.TinyDes;
                proInfo.FullDesRaw = fullDes;
                proInfo.PriceInfo = product.Price;
                proInfo.CatIDInfo = product.CatID;
                proInfo.QuantityInfo = product.Quantity;
                proInfo.NgayNhapInfo = product.NgayNhap.ToString();
                proInfo.SoLuotXemInfo = (int)product.SoLuotXem;
                proInfo.XuatXuInfo = product.XuatXu;
                proInfo.LoaiInfo = (int)product.Loai;
                proInfo.IDNhaSanXuatInfo = (int)product.IDNhaSanXuat;
                proInfo.BiXoaInfo = (byte)product.BiXoa;
                proInfo.SoLuongDaBanInfo = (int)product.SoLuongDaBan;

                return View(proInfo);
            }
        }
        //POST: Manageproduct/AjaxUpdatePro
        [HttpPost]
        public ActionResult AjaxUpdatePro(ProInfo proInfo, HttpPostedFileBase imgLg, HttpPostedFileBase imgSm)
        {
            ProInfoError ErrP = new ProInfoError();
            ErrP = AddHelpers.GetErrorProduct(proInfo);

            //Cap nhat thong tin khong cap nhat hinh anh
            if (imgLg == null && imgSm == null)
            {

                if (ErrP.ErrorProNameInfo != null || ErrP.ErrorXuatXuInfo != null || ErrP.ErrorNgayNhapInfo != null || ErrP.ErrorTinyDesInfo != null || ErrP.ErrorImgLgInfo != null || ErrP.ErrorImgSmInfo != null || ErrP.ErrorFullDesInfo != null)
                {
                    Thread.Sleep(2000);
                    return Json(ErrP);
                }
                else
                {
                    //Add Product
                    using (var dc = new QLBHEntities())
                    {
                        //Neu chuoi ngay nhap vao dang: "d/m/yyyy h:m:s"
                        //Thi chi lay: d/m/yyyy de paParseExact qua DateTime
                        string dateIn = null;
                        if (proInfo.NgayNhapInfo.Length > 10)
                        {
                            dateIn = proInfo.NgayNhapInfo.Remove(10);
                        }
                        else
                        {
                            dateIn = proInfo.NgayNhapInfo;
                        }

                        DateTime dt = DateTime.ParseExact(dateIn, "d/M/yyyy", null);

                        var product = dc.Products.Where(p => p.ProID == proInfo.ProIDInfo).FirstOrDefault();
                        product.ProName = proInfo.ProNameInfo;
                        product.TinyDes = proInfo.TinyDesInfo;
                        product.FullDes = proInfo.FullDesRaw;
                        product.Price = proInfo.PriceInfo;
                        product.CatID = proInfo.CatIDInfo;
                        product.Quantity = proInfo.QuantityInfo;
                        product.NgayNhap = dt;
                        product.SoLuotXem = 0;
                        product.XuatXu = proInfo.XuatXuInfo;
                        product.Loai = proInfo.LoaiInfo;
                        product.IDNhaSanXuat = proInfo.IDNhaSanXuatInfo;
                        product.BiXoa = 0;
                        product.SoLuongDaBan = proInfo.SoLuongDaBanInfo;

                        dc.SaveChanges();
                        
                        Thread.Sleep(2000);
                        return Json(ErrP);
                    }
                }
            }

            //Cap nhat có hình anh
            else
            {
                if (imgLg == null)
                {
                    ErrP.ErrorImgLgInfo = "Chọn hình lớn";
                }
                if (imgSm == null)
                {
                    ErrP.ErrorImgSmInfo = "Chọn hình phụ";
                }

                if (ErrP.ErrorProNameInfo != null || ErrP.ErrorXuatXuInfo != null || ErrP.ErrorNgayNhapInfo != null || ErrP.ErrorTinyDesInfo != null || ErrP.ErrorImgLgInfo != null || ErrP.ErrorImgSmInfo != null || ErrP.ErrorFullDesInfo != null)
                {
                    Thread.Sleep(2000);
                    return Json(ErrP);
                }
                else
                {
                    //Add Product
                    using (var dc = new QLBHEntities())
                    {
                        //Neu chuoi ngay nhap vao dang: "d/m/yyyy h:m:s"
                        //Thi chi lay: d/m/yyyy de paParseExact qua DateTime
                        string dateIn = null;
                        if (proInfo.NgayNhapInfo.Length > 10)
                        {
                            dateIn = proInfo.NgayNhapInfo.Remove(10);
                        }
                        else
                        {
                            dateIn = proInfo.NgayNhapInfo;
                        }

                        DateTime dt = DateTime.ParseExact(dateIn, "d/M/yyyy", null);

                        var product = dc.Products.Where(p => p.ProID == proInfo.ProIDInfo).FirstOrDefault();
                        product.ProName = proInfo.ProNameInfo;
                        product.TinyDes = proInfo.TinyDesInfo;
                        product.FullDes = proInfo.FullDesRaw;
                        product.Price = proInfo.PriceInfo;
                        product.CatID = proInfo.CatIDInfo;
                        product.Quantity = proInfo.QuantityInfo;
                        product.NgayNhap = dt;
                        product.SoLuotXem = 0;
                        product.XuatXu = proInfo.XuatXuInfo;
                        product.Loai = proInfo.LoaiInfo;
                        product.IDNhaSanXuat = proInfo.IDNhaSanXuatInfo;
                        product.BiXoa = 0;
                        product.SoLuongDaBan = proInfo.SoLuongDaBanInfo;

                        dc.SaveChanges();

                        //Xoa thu muc chu 2 hinh cũ
                        Ulti.DeleteProductImgs(proInfo.ProIDInfo, Server.MapPath("~"));

                        //Tao thu moi add 2 hinh moi vao
                        Ulti.SaveProductImgs(proInfo.ProIDInfo, Server.MapPath("~"), imgLg, imgSm);

                        Thread.Sleep(2000);
                        return Json(ErrP);
                    }
                }
            }
        }

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
