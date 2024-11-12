using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public static class clsSettingsDB
    {
        public static int GetDefaultFinePerDay()
        {
            int fine = -1;
            string query = "Select DefaultFinePerDay from Settings";
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            fine = int.Parse(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.ToString());
            }
            return fine;
        }

        public static int GetDefaultBorrowDays()
        {
            int days = -1;
            string query = "select DefaultBorrowDays from Settings";
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            days = int.Parse(result.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.ToString());
            }
            return days;
        }
    }
}
