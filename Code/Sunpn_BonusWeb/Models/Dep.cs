using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class Dep
    {
        public int depId { get; set; }
        public string depName { get; set; }
        public string compid { get; set; }
        public int sonCount { get; set; }
    }
}