using Database_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsSettings
    {
        public int DefaultBorrrowDays {  get; set; }
        public int DefaultFinePerDay {  get; set; }
        public clsSettings()
        {

        }

        public static int GetDefaultFinePerDay()
        {
            return clsSettingsDB.GetDefaultFinePerDay();
        }
    }
}
