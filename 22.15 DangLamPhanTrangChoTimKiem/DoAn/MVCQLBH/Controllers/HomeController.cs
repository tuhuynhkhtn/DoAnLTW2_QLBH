using MVCQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult HienThiSPMoiNhat()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.BiXoa == 0)
                    .OrderBy(p => p.NgayNhap)
                    .OrderByDescending(p => p.NgayNhap)
                    .Take(10)
                    .ToList();
                return PartialView("_PartialHienThi10SP", l);
            }
        }

        // GET: Home
        public ActionResult HienThiSPBanChayNhat()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.BiXoa == 0)
                    .OrderBy(p => p.SoLuongDaBan)
                    .OrderByDescending(p => p.SoLuongDaBan)
                    .Take(10)
                    .ToList();
                return PartialView("_PartialHienThi10SP", l);
            }
        }

        // GET: Home
        public ActionResult HienThiSPDuocXemNhieuNhat()
        {
            using (var dc = new QLBHEntities())
            {
                var l = dc.Products
                    .Where(p => p.BiXoa == 0)
                    .OrderBy(p => p.SoLuotXem)
                    .OrderByDescending(p => p.SoLuotXem)
                    .Take(10)
                    .ToList();
                return PartialView("_PartialHienThi10SP", l);
            }
        }


    }
}