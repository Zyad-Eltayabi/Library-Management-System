using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsFine
    {
        public int FineID { get; set; }
        public int UserID { get; set; }
        public int BorrowingRecordID { get; set; }
        public short NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public clsBorrowingRecord BorrowingRecord { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsFine(int userID, int borrowingRecordID, short numberOfLateDays, decimal fineAmount,
            bool paymentStatus)
        {
            UserID = userID;
            BorrowingRecordID = borrowingRecordID;
            NumberOfLateDays = numberOfLateDays;
            FineAmount = fineAmount;
            PaymentStatus = paymentStatus;
            BorrowingRecord = clsBorrowingRecord.GetBorrowingRecordByID(borrowingRecordID);
            this.enMode = Mode.Add;
        }

        public clsFine(int fineID, int userID, int borrowingRecordID, short numberOfLateDays, decimal fineAmount,
            bool paymentStatus)
        {
            FineID = fineID;
            UserID = userID;
            BorrowingRecordID = borrowingRecordID;
            NumberOfLateDays = numberOfLateDays;
            FineAmount = fineAmount;
            PaymentStatus = paymentStatus;
            BorrowingRecord = clsBorrowingRecord.GetBorrowingRecordByID(borrowingRecordID);
            this.enMode = Mode.Update;
        }

        private bool AddNewFine()
        {
            this.FineID = clsFineDB.AddNewFine(UserID, BorrowingRecordID, NumberOfLateDays, FineAmount, PaymentStatus);
            return FineID != -1;
        }

        private bool UpdateFine()
        {
            return clsFineDB.UpdateFine(FineID, UserID, BorrowingRecordID, NumberOfLateDays, FineAmount, PaymentStatus);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewFine();
                case Mode.Update:
                    return UpdateFine();
                default:
                    return false;
            }
        }

        public static DataTable GetAllFines()
        {
            return clsFineDB.GetAllFines();
        }

        public static bool DoesFineExist(int fineID)
        {
            return clsFineDB.DoesFineExist(fineID);
        }

        public static clsFine GetFineByID(int fineID)
        {
            int userID = -1;
            int borrowingRecordID = -1;
            short numberOfLateDays = -1;
            decimal fineAmount = 0; bool paymentStatus = false;

            if (clsFineDB.GetFineByID(fineID, ref userID, ref borrowingRecordID, ref numberOfLateDays, ref fineAmount, ref paymentStatus))
                return new clsFine(fineID, userID, borrowingRecordID, numberOfLateDays, fineAmount, paymentStatus);

            return null;
        }
    }
}
