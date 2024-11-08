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

        public static bool GetBorrowingRecordByID(int borrowingRecordID, ref int userID, ref int copyID, ref DateTime borrowingDate, ref DateTime dueDate, ref DateTime? actualReturnDate)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM BorrowingRecords WHERE BorrowingRecordID = @BorrowingRecordID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("BorrowingRecordID", borrowingRecordID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                userID = (int)sqlDataReader["UserID"];
                                copyID = (int)sqlDataReader["CopyID"];
                                borrowingDate = (DateTime)sqlDataReader["BorrowingDate"];
                                dueDate = (DateTime)sqlDataReader["DueDate"];

                                if (!string.IsNullOrWhiteSpace(sqlDataReader["ActualReturnDate"].ToString()))
                                    actualReturnDate = (DateTime)sqlDataReader["ActualReturnDate"];

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

        public static bool DoesBorrowingRecordExist(int borrowingRecordID)
        {
            bool isFound = false;
            string query = @"SELECT BorrowingRecordID FROM BorrowingRecords WHERE BorrowingRecordID = @BorrowingRecordID";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("BorrowingRecordID", borrowingRecordID);

                        object result = sqlCommand.ExecuteScalar();

                        isFound = (result != null);
                    }
                }
                SqlParameter sqlParameter = new SqlParameter();
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }
            return isFound;
        }

        public static DataTable GetAllBorrowingRecords()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SP_GetBorrowingRecords", sqlConnection))
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

        public static bool UpdateBorrowingRecord(int borrowingRecordID, int userID, int copyID, DateTime borrowingDate, DateTime dueDate, DateTime? actualReturnDate)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[BorrowingRecords]
                                              SET  [UserID] = @UserID,
                                                     [CopyID] = @CopyID,
                                                     [BorrowingDate] = @BorrowingDate,
                                                     [DueDate] = @DueDate,
                                                     [ActualReturnDate] = @ActualReturnDate
                                         WHERE BorrowingRecordID = @BorrowingRecordID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@BorrowingRecordID", borrowingRecordID);
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
                        sqlCommand.Parameters.AddWithValue("@CopyID", copyID);
                        sqlCommand.Parameters.AddWithValue("@BorrowingDate", borrowingDate);
                        sqlCommand.Parameters.AddWithValue("@DueDate", dueDate);
                        sqlCommand.Parameters.AddWithValue("@ActualReturnDate", actualReturnDate ?? (object)DBNull.Value);


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

        public static bool DeleteBorrowingRecord(int borrowingRecordID)
        {
            string query = @"delete from BorrowingRecords where BorrowingRecordID = @BorrowingRecordID";
            int rowsAffected = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@BorrowingRecordID", borrowingRecordID);
                        rowsAffected = (int)sqlCommand.ExecuteNonQuery();
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
