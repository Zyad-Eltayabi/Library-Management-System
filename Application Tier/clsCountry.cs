using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Tier
{
    public class clsCountry
    {
        public static DataTable GetAllCountries()
        {
            return clsCountryDB.GetAllCountries();
        }

        public static string GetCountryName(int countryID)
        {
            return clsCountryDB.GetCountryName(countryID);
        }
    }
}
