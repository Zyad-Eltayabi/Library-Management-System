using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public class clsBorrowingRecordsDB
    {
        public static int AddNewBorrowingRecord(int userID, int copyID, DateTime borrowingDate, DateTime dueDate, DateTime? actualReturnDate)
        {
            int borrowingRecordID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[BorrowingRecords]
                                                    ([UserID],[CopyID],[BorrowingDate],[DueDate],[ActualReturnDate])
                                                     VALUES
                                                      (@UserID,@CopyID,@BorrowingDate,@DueDate,@ActualReturnDate);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
                        sqlCommand.Parameters.AddWithValue("@CopyID", copyID);
                        sqlCommand.Parameters.AddWithValue("@BorrowingDate", borrowingDate);
                        sqlCommand.Parameters.AddWithValue("@DueDate", dueDate);
                        sqlCommand.Parameters.AddWithValue("@ActualReturnDate", actualReturnDate ?? (object)DBNull.Value);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            borrowingRecordID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);
            }

            return borrowingRecordID;
        }

    }
}
