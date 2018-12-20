using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class LongShouquan
    {
        public string access_token { get; set; }
        //public int expires_in { get; set; }
        public string permanent_code { get; set; }
        public auth_corp_info auth_corp_info { get; set; } = new auth_corp_info();
        public auth_user_info auth_user_info { get; set; } = new auth_user_info();

        //public string corpid { get; set; }
        //public string corp_name { get; set; }
        //public string corp_type { get; set; }
        //public string corp_square_logo_url { get; set; }
        //public int corp_user_max { get; set; }
        //public int corp_agent_max { get; set; }
        //public string corp_full_name { get; set; }
        //public int verified_end_time { get; set; }
        //public int subject_type { get; set; }
        //public string corp_wxqrcode { get; set; }
        //public string corp_scale { get; set; }
        //public string corp_industry { get; set; }
        //public string corp_sub_industry { get; set; }
        //public string auth_info { get; set; }
        //public string agent { get; set; }
        //public string name { get; set; }
        //public string square_logo_url { get; set; }
        //public string round_logo_url { get; set; }
        //public int appid { get; set; }
        //public string privilege { get; set; }
        //public string allow_party { get; set; }
        //public string allow_tag { get; set; }
        //public string allow_user { get; set; }
        //public string extra_party { get; set; }
        //public string extra_user { get; set; }
        //public string extra_tag { get; set; }
        //public string level { get; set; }
        //public string auth_user_info { get; set; }
        //public string userid { get; set; }



    }

    /// <summary>
    /// 授权方企业信息
    /// </summary>
    public class auth_corp_info
    {
        public string corpid { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string corp_name { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string corp_full_name { get; set; }//

        /// <summary>
        /// 公司logo
        /// </summary>
        public string corp_square_logo_url { get; set; }
    }

    /// <summary>
    /// 授权管理员信息
    /// </summary>
    public class auth_user_info
    {
        /// <summary>
        /// 授权管理员id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 授权管理员的名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 授权管理员的头像
        /// </summary>
        public string avatar { get; set; }
    }
}