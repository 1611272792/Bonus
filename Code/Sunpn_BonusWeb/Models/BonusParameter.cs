using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class BonusParameter
    {
        private int bonusSetID;
        public int BonusSetID
        {
            get { return bonusSetID; }
            set { bonusSetID = value; }
        }

        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        private int bonusNum;
        public int BonusNum
        {
            get { return bonusNum; }
            set { bonusNum = value; }
        }

        private int isOrNo;
        public int IsOrNo
        {
            get { return isOrNo; }
            set { isOrNo = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private int companyId;
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
    }
}