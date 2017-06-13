using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCQLBH.Models
{
    public class PasswordError
    {
        public string ErrorPasswordOld { get; set; }

        public string ErrorPasswordNew { get; set; }

        public string ErrorPasswordRetype { get; set; }
    }
}