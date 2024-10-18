using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;


namespace Database_Tier
{
    public class clsAuthorsDB
    {
        public static int AddNewAuthor(string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath, bool gender, int nationalityID)
        {
            int authorID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Authors]
                                                    ([FirstName],[LastName],[DateOfBirth],[DateOfDeath],[Gender],[NationalityID])
                                                     VALUES
                                                      (@FirstName,@LastName,@DateOfBirth,@DateOfDeath,@Gender,@NationalityID);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                        sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                        sqlCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
                        sqlCommand.Parameters.AddWithValue("@NationalityID", nationalityID);
                        if (dateOfDeath == null)
                        {
                            sqlCommand.Parameters.AddWithValue("@DateOfDeath",DBNull.Value);
                        }
                        else
                        {
                            sqlCommand.Parameters.AddWithValue("@DateOfDeath", dateOfDeath);
                        }


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            authorID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return authorID;
        }

        public static DataTable GetAllAuthors()
        {
            DataTable dataTable = new DataTable();

            string query = @" SELECT *  FROM Authors ";

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
