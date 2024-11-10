using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public static class clsFinesDB
    {
        public static int AddNewFine(int userID, int borrowingRecordID, short numberOfLateDays, decimal fineAmount, bool paymentStatus)
        {
            int fineID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Fines]
                                                    ([UserID],[BorrowingRecordID],[NumberOfLateDays],[FineAmount],[PaymentStatus])
                                                     VALUES
                                                      (@UserID,@BorrowingRecordID,@NumberOfLateDays,@FineAmount,@PaymentStatus);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
                        sqlCommand.Parameters.AddWithValue("@BorrowingRecordID", borrowingRecordID);
                        sqlCommand.Parameters.AddWithValue("@NumberOfLateDays", numberOfLateDays);
                        sqlCommand.Parameters.AddWithValue("@FineAmount", fineAmount);
                        sqlCommand.Parameters.AddWithValue("@PaymentStatus", paymentStatus);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            fineID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return fineID;


        }

    }
}
