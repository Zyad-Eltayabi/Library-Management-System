using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Database_Tier


{
    public class clsAdminDB
    {
        public static int AddNewAdmin(string userName, string password, bool isActive, string fullName)
        {
            int adminID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Admins]
                                                    ( [UserName], [Password], [IsActive], [FullName])
                                                     VALUES
                                                      (@UserName, @Password, @IsActive, @FullName);
                                                        select SCOPE_IDENTITY();";


            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserName", userName);
                        sqlCommand.Parameters.AddWithValue("@Password", password);
                        sqlCommand.Parameters.AddWithValue("@IsActive", isActive);
                        sqlCommand.Parameters.AddWithValue("@FullName", fullName ?? (object)DBNull.Value);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            adminID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);
            }

            return adminID;
        }

        public static DataTable GetAllAdmins()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SP_GetAllAdmins", sqlConnection))
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

        public static bool DeleteAdmin(int adminID)
        {
            string query = @"delete from Admins where AdminID = @AdminID";
            int rowsAffected = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@AdminID", adminID);
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

        public static bool GetAdminByID(int adminID, ref string fullName, ref string userName, ref string password, ref bool isActive)
        {
            bool isFound = false;
            string query = "select top 1 * from Admins where AdminID = @AdminID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("@AdminID", adminID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                fullName = (string)sqlDataReader["FullName"];
                                userName = (string)sqlDataReader["UserName"];
                                password = (string)sqlDataReader["Password"];
                                isActive = (bool)sqlDataReader["IsActive"];
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

        public static bool UpdateAdmin(int adminID, string fullName, string userName, string password, bool isActive)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[Admins]
                                              SET  
                                                    [FullName] = @FullName,
                                                    [UserName] = @UserName,
                                                    [Password] = @Password,
                                                    [IsActive] = @IsActive
                                         WHERE AdminID = @AdminID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@AdminID", adminID);
                        sqlCommand.Parameters.AddWithValue("@FullName", fullName ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@UserName", userName);
                        sqlCommand.Parameters.AddWithValue("@Password", password);
                        sqlCommand.Parameters.AddWithValue("@IsActive", isActive);


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

        public static DataTable GetAdminByUserNameAndPassword(string userName,string password)
        {
            DataTable dataTable = new DataTable();

            string query = @"SELECT TOP 1 * FROM Admins WHERE UserName = @UserName AND Password = @Password";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserName",userName);
                        sqlCommand.Parameters.AddWithValue("@Password", password);

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

        public static DataTable GetAdminsCount()
        {
            DataTable dataTable = new DataTable();
            string query = @"select TotalAdmins = COUNT(Admins.AdminID) from Admins";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
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

    }
}
