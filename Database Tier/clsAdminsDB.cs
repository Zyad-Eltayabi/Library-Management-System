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
    public class clsAdminsDB
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
                Console.WriteLine(ex.ToString());

            }

            return adminID;
        }
    }
}
