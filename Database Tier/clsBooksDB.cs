using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public class clsBooksDB
    {
        public static int AddNewBook(string title, string iSBN, DateTime publicationDate, string genre, string additionalDetails, string bookImage, int authorID)
        {
            int bookID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Books]
                                                    ([Title],[ISBN],[PublicationDate],[Genre],[AdditionalDetails],[BookImage],[AuthorID])
                                                     VALUES
                                                      (@Title,@ISBN,@PublicationDate,@Genre,@AdditionalDetails,@BookImage,@AuthorID);
                                                        select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@Title", title);
                        sqlCommand.Parameters.AddWithValue("@ISBN", iSBN);
                        sqlCommand.Parameters.AddWithValue("@PublicationDate", publicationDate);
                        sqlCommand.Parameters.AddWithValue("@Genre", genre);
                        sqlCommand.Parameters.AddWithValue("@AdditionalDetails", additionalDetails ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@BookImage", bookImage ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@AuthorID", authorID);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            bookID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return bookID;


        }

    }
}
