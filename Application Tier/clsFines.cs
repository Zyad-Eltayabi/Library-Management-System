using Database_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsFines
    {
        public int FineID { get; set; }
        public int UserID { get; set; }
        public int BorrowingRecordID { get; set; }
        public short NumberOfLateDays { get; set; }
        public decimal FineAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public clsBorrowingRecords BorrowingRecord { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsFines(int userID, int borrowingRecordID, short numberOfLateDays, decimal fineAmount,
            bool paymentStatus)
        {
            UserID = userID;
            BorrowingRecordID = borrowingRecordID;
            NumberOfLateDays = numberOfLateDays;
            FineAmount = fineAmount;
            PaymentStatus = paymentStatus;
            BorrowingRecord = clsBorrowingRecords.GetBorrowingRecordByID(borrowingRecordID);
            this.enMode = Mode.Add;
        }

        private bool AddNewFine()
        {
            this.FineID = clsFinesDB.AddNewFine(UserID, BorrowingRecordID, NumberOfLateDays, FineAmount, PaymentStatus);
            return FineID != -1;
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewFine();
                case Mode.Update:
                //return ;
                default:
                    return false;
            }
        }
    }
}
