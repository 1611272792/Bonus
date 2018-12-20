using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class BonusItems
    {
        public string BonusItemsId { get; set; }//奖金项id
        public string BonusName { get; set; }//奖金项名称
        public double BonusMoney { get; set; }//奖金项可用金额
        public int BonusType { get; set; }//奖金项类型


    }
}