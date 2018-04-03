﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussiness.Models
{
    public class UserModel : BaseModel
    {
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}