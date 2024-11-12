﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Tier
{
    public class clsReservationDB
    {
        public static int AddNewReservation(int userID, int copyID, DateTime reservationDate, bool isBorrowed, bool isReturned)
        {
            int reservationID = -1;
            string query = @" USE [LibraryManagementSystem] 
                                                   INSERT INTO [dbo].[Reservations]
                                                    ([UserID],[CopyID],[ReservationDate],[IsBorrowed],[IsReturned])
                                                     VALUES
                                                      (@UserID,@CopyID,@ReservationDate,@IsBorrowed,@IsReturned);
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
                        sqlCommand.Parameters.AddWithValue("@ReservationDate", reservationDate);
                        sqlCommand.Parameters.AddWithValue("@IsBorrowed", isBorrowed);
                        sqlCommand.Parameters.AddWithValue("@IsReturned", isReturned);


                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            reservationID = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return reservationID;


        }

        public static int GetLatestReservationIdOnBookCopy(int bookCopyID)
        {
            int reservationId = -1;
            string query = @"select top 1 ReservationID from Reservations 
                            where CopyID = @CopyID
                            order by ReservationID desc";
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@CopyID", bookCopyID);

                        object result = sqlCommand.ExecuteScalar();
                        if (result != null)
                            reservationId = int.Parse(result.ToString());

                    }
                }
            }


            catch (Exception ex)
            {
                clsErrorLog.Log(ex.Message);

            }

            return reservationId;
        }

        public static bool GetReservationByID(int reservationID, ref int userID, ref int copyID, ref DateTime reservationDate, ref bool isBorrowed, ref bool isReturned)
        {
            bool isFound = false;
            string query = "SELECT TOP 1 * FROM Reservations WHERE ReservationID = @ReservationID";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("ReservationID", reservationID);

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            if (sqlDataReader.Read())
                            {
                                isFound = true;
                                userID = (int)sqlDataReader["UserID"];
                                copyID = (int)sqlDataReader["CopyID"];
                                reservationDate = (DateTime)sqlDataReader["ReservationDate"];
                                isBorrowed = (bool)sqlDataReader["IsBorrowed"];
                                isReturned = (bool)sqlDataReader["IsReturned"];

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

    }
}
