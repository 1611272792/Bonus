using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class BonusOut
    {
        public string BonusItemId { get; set; }//奖金项id
        public string DisMan { get; set; }//发奖金的人
        public string EarMan { get; set; }//接收奖金的人
        public double EarMoney { get; set; }//发奖金的金额
        public int BonusType { get; set; }//发奖金的类型0部门奖金1发放的个人奖金2交易
        public int IsGet { get; set; }
        public string ResonID { get; set; }//原因id
        public int PersonCount { get; set; }//传进来的人的个数
        public string ResonContent { get; set; }//原因文字内容
        public string ResonContentImg { get; set; }//原因图片
        public int ResonCount { get; set; }//图片个数
        public int BonusTypes { get; set; }//看是奖金负责人发还是授权人发
        public string companyid { get; set; }//公司id
    }
}