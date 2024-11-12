using Database_Tier;
using System;
using System.Collections.Generic;
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
        DateTime ReservationDate { get; set; }
        public bool IsBorrowed { get; set; }
        public bool IsReturned { get; set; }
        clsBookCopy bookCopy { get; set; }
        clsUser user { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsReservation(int userID, int copyID, DateTime reservationDate,bool isBorrowed,bool isReturned)
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

        private bool AddNewReservation()
        {
            this.ReservationID = clsReservationDB.AddNewReservation(UserID, CopyID, ReservationDate, IsBorrowed, IsReturned);
            return ReservationID != -1;
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewReservation();
                case Mode.Update:
                default:
                    return false;
            }
        }
    }
}
