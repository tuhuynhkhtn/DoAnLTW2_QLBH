using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCQLBH.Models
{
    public class ProInfo
    {
        public int ProIDInfo { get; set; }
        public string ProNameInfo { get; set; }
        public string TinyDesInfo { get; set; }

        //public string FullDesInfo { get; set; }
        [AllowHtml]
        public string FullDesRaw { get; set; }

        public decimal PriceInfo { get; set; }
        public int CatIDInfo { get; set; }
        public int QuantityInfo { get; set; }
        public DateTime NgayNhapInfo { get; set; }
        public int SoLuotXemInfo { get; set; }
        public string XuatXuInfo { get; set; }
        public string LoaiInfo { get; set; }
        public int IDNhaSanXuatInfo { get; set; }
        public byte BiXoaInfo { get; set; }
        public int SoLuongDaBanInfo { get; set; }

        public HttpPostedFileBase HinhChinh { get; set; }
        public HttpPostedFileBase HinhPhu { get; set; }

    }
}