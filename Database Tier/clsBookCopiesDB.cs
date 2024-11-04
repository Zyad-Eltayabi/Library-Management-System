using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public class clsBookCopiesDB
    {
        public static int AddNewBookCopy(int bookID, bool availabilityStatus)
        {
            int bookCopyID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[BookCopies]
                                                    ([BookID],[AvailabilityStatus])
                                                     VALUES
                                                      (@BookID,@AvailabilityStatus);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@BookID", bookID);
                        sqlCommand.Parameters.AddWithValue("@AvailabilityStatus", availabilityStatus);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            bookCopyID = int.Parse(result.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);
            }

            return bookCopyID;

        }

    }
}
