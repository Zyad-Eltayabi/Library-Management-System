﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public class clsBooksDB
    {
        public static int AddNewBook(string title, string iSBN, DateTime publicationDate, string genre, string additionalDetails,
            string bookImage, int authorID)
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

        public static DataTable GetAllBooks()
        {
            DataTable dataTable = new DataTable();

            string query = @" SELECT *  FROM Books ";

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

        public static bool DoesBookExist(int bookID)
        {
            bool isFound = false;
            string query = @"SELECT BookID FROM Books WHERE BookID = @BookID";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("BookID", bookID);

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

        public static bool GetBookByID(int bookID, ref string title, ref string iSBN, ref DateTime publicationDate, ref string genre,
            ref string additionalDetails, ref string bookImage, ref int authorID)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM Books WHERE BookID = @BookID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("BookID", bookID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                title = (string)sqlDataReader["Title"];
                                iSBN = (string)sqlDataReader["ISBN"];
                                publicationDate = (DateTime)sqlDataReader["PublicationDate"];
                                genre = (string)sqlDataReader["Genre"];
                                additionalDetails = (string)sqlDataReader["AdditionalDetails"];
                                bookImage = (string)sqlDataReader["BookImage"];
                                authorID = (int)sqlDataReader["AuthorID"];

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

        public static bool UpdateBook(int bookID, string title, string iSBN, DateTime publicationDate, string genre,
            string additionalDetails, string bookImage, int authorID)
        {
            int rowsAffected = 0;
            string query = @"USE [LibraryManagementSystem]
                                        UPDATE [dbo].[Books]
                                              SET  [Title] = @Title,
                                             [ISBN] = @ISBN,
                                             [PublicationDate] = @PublicationDate,
                                             [Genre] = @Genre,
                                             [AdditionalDetails] = @AdditionalDetails,
                                             [BookImage] = @BookImage,
                                             [AuthorID] = @AuthorID
                                         WHERE BookID = @BookID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@BookID", bookID);
                        sqlCommand.Parameters.AddWithValue("@Title", title);
                        sqlCommand.Parameters.AddWithValue("@ISBN", iSBN);
                        sqlCommand.Parameters.AddWithValue("@PublicationDate", publicationDate);
                        sqlCommand.Parameters.AddWithValue("@Genre", genre);
                        sqlCommand.Parameters.AddWithValue("@AdditionalDetails", additionalDetails ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@BookImage", bookImage ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@AuthorID", authorID);


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
