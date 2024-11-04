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
    public class clsBookCopiesDB
    {
        public static int AddNewBookCopy(int bookID, bool availabilityStatus)
        {
            int bookCopyID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[BookCopies]
                                                    ([BookID],[AvailabilityStatus])
                                                     VALUES
                                                      (@BookID,@AvailabilityStatus);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@BookID", bookID);
                        sqlCommand.Parameters.AddWithValue("@AvailabilityStatus", availabilityStatus);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            bookCopyID = int.Parse(result.ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);
            }

            return bookCopyID;

        }

        public static DataTable GetAllBookCopies()
        {
            DataTable dataTable = new DataTable();

            string query = @"SELECT      BookCopies.CopyID, Books.Title, Books.ISBN, Books.Genre, Authors.AuthorID, Authors.FirstName,
                            Authors.LastName,BookCopies.AvailabilityStatus
                            FROM          Books INNER JOIN
                              BookCopies ON Books.BookID = BookCopies.BookID INNER JOIN
                              Authors ON Books.AuthorID = Authors.AuthorID";

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
