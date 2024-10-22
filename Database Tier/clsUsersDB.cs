using System;
using System.Collections.Generic;
using System.Configuration;
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

    }
}
