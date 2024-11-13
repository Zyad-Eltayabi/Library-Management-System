using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsReservation
    {
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public int CopyID { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsBorrowed { get; set; }
        public bool IsReturned { get; set; }
        public clsBookCopy bookCopy { get; set; }
        public clsUser user { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsReservation(int userID, int copyID, DateTime reservationDate, bool isBorrowed, bool isReturned)
        {
            UserID = userID;
            CopyID = copyID;
            ReservationDate = reservationDate;
            IsBorrowed = isBorrowed;
            IsReturned = isReturned;
            this.bookCopy = clsBookCopy.GetBookCopyByID(copyID);
            this.user = clsUser.GetUserByID(userID);
            enMode = Mode.Add;
        }

        private clsReservation(int reservationID, int userID, int copyID, DateTime reservationDate, bool isBorrowed, bool isReturned)
        {
            ReservationID = reservationID;
            UserID = userID;
            CopyID = copyID;
            ReservationDate = reservationDate;
            IsBorrowed = isBorrowed;
            IsReturned = isReturned;
            this.bookCopy = clsBookCopy.GetBookCopyByID(copyID);
            this.user = clsUser.GetUserByID(userID);
            enMode = Mode.Update;
        }

        private bool AddNewReservation()
        {
            this.ReservationID = clsReservationDB.AddNewReservation(UserID, CopyID, ReservationDate, IsBorrowed, IsReturned);
            return ReservationID != -1;
        }

        private bool UpdateReservation()
        {
            return clsReservationDB.UpdateReservation(ReservationID, UserID, CopyID, ReservationDate, IsBorrowed, IsReturned);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewReservation();
                case Mode.Update:
                    return UpdateReservation();
                default:
                    return false;
            }
        }

        public static int GetLatestReservationIdOnBookCopy(int bookCopyID)
        {
            return clsReservationDB.GetLatestReservationIdOnBookCopy(bookCopyID);
        }

        public static clsReservation GetReservationRecordByID(int ReservationID)
        {
            int userID = -1;
            int copyID = -1;
            DateTime reservationDate = DateTime.Now;
            bool isBorrowed = false;
            bool isReturned = false;

            if (clsReservationDB.GetReservationByID(ReservationID, ref userID, ref copyID, ref reservationDate, ref isBorrowed, ref isReturned))
                return new clsReservation(ReservationID, userID, copyID, reservationDate, isBorrowed, isReturned);

            return null;
        }

        public static DataTable GetAllReservations()
        {
            return clsReservationDB.GetAllReservations();
        }

        public static DataTable GetReservationsCount()
        {
            return clsReservationDB.GetReservationsCount();
        }
    }
}
