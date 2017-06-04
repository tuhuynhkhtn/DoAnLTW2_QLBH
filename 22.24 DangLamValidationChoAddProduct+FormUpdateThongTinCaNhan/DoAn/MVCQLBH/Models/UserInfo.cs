﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCQLBH.Models
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int Permission { get; set; }

        public string PasswordRetype { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DOB { get; set; }
    }
}