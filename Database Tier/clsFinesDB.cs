using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public static DataTable GetAllFines()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SP_GetFines", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dataTable.Load(sqlDataReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                clsErrorLog.Log(ex.Message);
            }

            return dataTable;
        }

        public static bool DoesFineExist(int fineID)
        {
            bool isFound = false;
            string query = @"SELECT FineID FROM Fines WHERE FineID = @FineID";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("FineID", fineID);

                        object result = sqlCommand.ExecuteScalar();

                        isFound = (result != null);
                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }
            return isFound;
        }

        public static bool GetFineByID(int fineID, ref int userID, ref int borrowingRecordID, ref short numberOfLateDays, ref decimal fineAmount, ref bool paymentStatus)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM Fines WHERE FineID = @FineID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("FineID", fineID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                userID = (int)sqlDataReader["UserID"];
                                borrowingRecordID = (int)sqlDataReader["BorrowingRecordID"];
                                numberOfLateDays = (short)sqlDataReader["NumberOfLateDays"];
                                fineAmount = (decimal)sqlDataReader["FineAmount"];
                                paymentStatus = (bool)sqlDataReader["PaymentStatus"];

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                clsErrorLog.Log(ex.Message);
            }

            return isFound;
        }

        public static bool UpdateFine(int fineID, int userID, int borrowingRecordID, short numberOfLateDays, decimal fineAmount, bool paymentStatus)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[Fines]
                                              SET  [UserID] = @UserID,
                                             [BorrowingRecordID] = @BorrowingRecordID,
                                             [NumberOfLateDays] = @NumberOfLateDays,
                                             [FineAmount] = @FineAmount,
                                             [PaymentStatus] = @PaymentStatus
                                         WHERE FineID = @FineID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@FineID", fineID);
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
                        sqlCommand.Parameters.AddWithValue("@BorrowingRecordID", borrowingRecordID);
                        sqlCommand.Parameters.AddWithValue("@NumberOfLateDays", numberOfLateDays);
                        sqlCommand.Parameters.AddWithValue("@FineAmount", fineAmount);
                        sqlCommand.Parameters.AddWithValue("@PaymentStatus", paymentStatus);


                        rowsAffected = int.Parse(sqlCommand.ExecuteNonQuery().ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                clsErrorLog.Log(ex.Message);
            }
            return rowsAffected > 0;
        }

    }
}
