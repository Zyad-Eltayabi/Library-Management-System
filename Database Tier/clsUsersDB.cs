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
    public class clsUsersDB
    {
        public static int AddNewUser(string libraryCardNumber, string firstName, string lastName, DateTime dateOfBirth, bool gender,
            string email, string phoneNumber, string address, DateTime membershipDate, int nationalityID)
        {
            int userID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Users]
                                                    ([LibraryCardNumber],[FirstName],[LastName],[DateOfBirth],[Gender],[Email],[PhoneNumber],[Address],[MembershipDate],[NationalityID])
                                                     VALUES
                                                      (@LibraryCardNumber,@FirstName,@LastName,@DateOfBirth,@Gender,@Email,@PhoneNumber,@Address,@MembershipDate,@NationalityID);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@LibraryCardNumber", libraryCardNumber);
                        sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                        sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                        sqlCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
                        sqlCommand.Parameters.AddWithValue("@Email", email);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        sqlCommand.Parameters.AddWithValue("@Address", address);
                        sqlCommand.Parameters.AddWithValue("@MembershipDate", membershipDate);
                        sqlCommand.Parameters.AddWithValue("@NationalityID", nationalityID);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            userID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return userID;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();

            string query = @" SELECT *  FROM Users ";

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

        public static bool GetUserByID(int userID, ref string libraryCardNumber, ref string firstName, ref string lastName, ref DateTime dateOfBirth, ref bool gender, ref string email, ref string phoneNumber, ref string address, ref DateTime membershipDate, ref int nationalityID)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM Users WHERE UserID = @UserID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("UserID", userID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                libraryCardNumber = (string)sqlDataReader["LibraryCardNumber"];
                                firstName = (string)sqlDataReader["FirstName"];
                                lastName = (string)sqlDataReader["LastName"];
                                dateOfBirth = (DateTime)sqlDataReader["DateOfBirth"];
                                gender = (bool)sqlDataReader["Gender"];
                                email = (string)sqlDataReader["Email"];
                                phoneNumber = (string)sqlDataReader["PhoneNumber"];
                                address = (string)sqlDataReader["Address"];
                                membershipDate = (DateTime)sqlDataReader["MembershipDate"];
                                nationalityID = (int)sqlDataReader["NationalityID"];
                                ;
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

        public static bool DoesUserExist(int userID)
        {
            bool isFound = false;
            string query = @"SELECT UserID FROM Users WHERE UserID = @UserID";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("UserID", userID);

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

        public static bool UpdateUser(int userID, string libraryCardNumber, string firstName, string lastName, DateTime dateOfBirth, bool gender, string email, string phoneNumber, string address, DateTime membershipDate, int nationalityID)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[Users]
                                              SET  [LibraryCardNumber] = @LibraryCardNumber,
                                             [FirstName] = @FirstName,
                                             [LastName] = @LastName,
                                             [DateOfBirth] = @DateOfBirth,
                                             [Gender] = @Gender,
                                             [Email] = @Email,
                                             [PhoneNumber] = @PhoneNumber,
                                             [Address] = @Address,
                                             [MembershipDate] = @MembershipDate,
                                             [NationalityID] = @NationalityID
                                         WHERE UserID = @UserID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
                        sqlCommand.Parameters.AddWithValue("@LibraryCardNumber", libraryCardNumber);
                        sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                        sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                        sqlCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
                        sqlCommand.Parameters.AddWithValue("@Email", email);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        sqlCommand.Parameters.AddWithValue("@Address", address);
                        sqlCommand.Parameters.AddWithValue("@MembershipDate", membershipDate);
                        sqlCommand.Parameters.AddWithValue("@NationalityID", nationalityID);


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

        public static bool DeleteUser(int userID)
        {
            string query = @"delete from Users where UserID = @UserID";
            int rowsAffected = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserID", userID);
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
