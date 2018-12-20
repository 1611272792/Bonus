using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sunpn_BonusWeb.Models
{
    public class BonusReson
    {
        private long iD;
        public long ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string bonusResonName;
        public string BonusResonName
        {
            get { return bonusResonName; }
            set { bonusResonName = value; }
        }

        private string bonusResonUserId;
        public string BonusResonUserId
        {
            get { return bonusResonUserId; }
            set { bonusResonUserId = value; }
        }
    }
}