using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Tier
{
    public class clsCountries
    {
        public static DataTable GetAllCountries()
        {
            return clsCountriesDB.GetAllCountries();
        }

        public static string GetCountryName(int countryID)
        {
            return clsCountriesDB.GetCountryName(countryID);
        }
    }
}
