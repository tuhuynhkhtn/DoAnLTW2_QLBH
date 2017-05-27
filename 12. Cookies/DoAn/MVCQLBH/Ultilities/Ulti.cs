using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace MVCQLBH.Ultilities
{
    public class Ulti
    {
        public static string Md5Hash(string str)
        {
            using (var md5 = MD5.Create()) // Phải thêm thư viện using System.Security.Cryptography; trước
            {
                byte[] arr = Encoding.ASCII.GetBytes(str);
                byte[] arrMd5 = md5.ComputeHash(arr);
                StringBuilder sb = new StringBuilder();
                foreach(var b in arrMd5)
                {
                    sb.AppendFormat("{0:X}", b);
                }
                return sb.ToString();
            }
        }
    }
}