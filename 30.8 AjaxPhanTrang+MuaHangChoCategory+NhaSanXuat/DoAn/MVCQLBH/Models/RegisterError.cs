using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCQLBH.Models
{
    public class RegisterError
    {
        public string ErrorUsername { get; set; }

        public string ErrorPassword { get; set; }

        public string ErrorPasswordRetype { get; set; }

        public string ErrorName { get; set; }

        public string ErrorEmail { get; set; }

        public string ErrorDOB { get; set; }

        public string ErrorCaptcha { get; set; }
    }
}