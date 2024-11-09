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
                            sqlCommand.Parameters.AddWithValue("@DateOfDeath", DBNull.Value);
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
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("SP_GetAllAuthors", sqlConnection))
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

        public static bool DeleteAuthor(int authorID)
        {
            string query = @"delete from Authors where AuthorID = @AuthorID";
            int rowsAffected = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@AuthorID", authorID);
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

        public static bool GetAuthorByID(int authorID, ref string firstName, ref string lastName, ref DateTime dateOfBirth, ref DateTime? dateOfDeath, ref bool gender, ref int nationalityID)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM Authors WHERE AuthorID = @AuthorID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("AuthorID", authorID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                firstName = (string)sqlDataReader["FirstName"];
                                lastName = (string)sqlDataReader["LastName"];
                                dateOfBirth = (DateTime)sqlDataReader["DateOfBirth"];

                                if (!String.IsNullOrEmpty(sqlDataReader["DateOfDeath"].ToString()))
                                    dateOfDeath = (DateTime)sqlDataReader["DateOfDeath"];

                                gender = (bool)sqlDataReader["Gender"];
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

        public static bool UpdateAuthor(int authorID, string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath, bool gender, int nationalityID)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[Authors]
                                              SET  [FirstName] = @FirstName,
                                                     [LastName] = @LastName,
                                                     [DateOfBirth] = @DateOfBirth,
                                                     [DateOfDeath] = @DateOfDeath,
                                                     [Gender] = @Gender,
                                                     [NationalityID] = @NationalityID
                                         WHERE AuthorID = @AuthorID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@AuthorID", authorID);
                        sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                        sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                        sqlCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                        if (dateOfDeath == null)
                        {
                            sqlCommand.Parameters.AddWithValue("@DateOfDeath", DBNull.Value);
                        }
                        else
                        {
                            sqlCommand.Parameters.AddWithValue("@DateOfDeath", dateOfDeath);
                        }

                        sqlCommand.Parameters.AddWithValue("@Gender", gender);
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

        public static DataTable GetAuthorsNames()
        {
            DataTable dataTable = new DataTable();

            string query = @"select AuthorID, FullName = FirstName + ' ' +  LastName from Authors";

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
