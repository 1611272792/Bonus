using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class Users
    {
        public string Shou { get; set; }
        public List<User> user { get; set; }
    }
    public class User
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public int isEmp { get; set; }
    }
}