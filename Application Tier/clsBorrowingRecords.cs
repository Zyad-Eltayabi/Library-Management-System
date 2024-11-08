using Database_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBorrowingRecords
    {
        public int BorrowingRecordID { get; set; }
        public int UserID { get; set; }
        public int CopyID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        clsBookCopies BookCopy { get; set; }
        clsUsers User { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsBorrowingRecords( int userID, int copyID, DateTime borrowingDate, DateTime dueDate, DateTime? actualReturnDate)
        {
            UserID = userID;
            CopyID = copyID;
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ActualReturnDate = actualReturnDate;
            BookCopy = clsBookCopies.GetBookCopyByID(copyID);
            User = clsUsers.GetUserByID(userID);
            this.enMode = Mode.Add;
        }

        private bool AddNewBorrowingRecord()
        {
            this.BorrowingRecordID = clsBorrowingRecordsDB.AddNewBorrowingRecord(UserID, CopyID,BorrowingDate,DueDate,ActualReturnDate);
            return this.BorrowingRecordID != -1;
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewBorrowingRecord();
                case Mode.Update:
                   // return false;
                default:
                   return false;
            }
        }
    }
}
