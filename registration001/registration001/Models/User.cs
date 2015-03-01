using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace registration001.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? maleGenger { get; set; }
    }
}