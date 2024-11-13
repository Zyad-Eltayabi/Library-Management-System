using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBorrowingRecord
    {
        public int BorrowingRecordID { get; set; }
        public int UserID { get; set; }
        public int CopyID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public clsBookCopy BookCopy { get; set; }
        public clsBook Book { get; set; }
        public clsUser User { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsBorrowingRecord(int userID, int copyID, DateTime borrowingDate, DateTime dueDate, DateTime? actualReturnDate)
        {
            UserID = userID;
            CopyID = copyID;
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ActualReturnDate = actualReturnDate;
            BookCopy = clsBookCopy.GetBookCopyByID(copyID);
            User = clsUser.GetUserByID(userID);
            Book = clsBook.GetBookByID(BookCopy.BookID);
            this.enMode = Mode.Add;
        }

        private clsBorrowingRecord(int borrowingRecordID, int userID, int copyID, DateTime borrowingDate, DateTime dueDate,
            DateTime? actualReturnDate)
        {
            BorrowingRecordID = borrowingRecordID;
            UserID = userID;
            CopyID = copyID;
            BorrowingDate = borrowingDate;
            DueDate = dueDate;
            ActualReturnDate = actualReturnDate;
            BookCopy = clsBookCopy.GetBookCopyByID(copyID);
            User = clsUser.GetUserByID(userID);
            Book = clsBook.GetBookByID(BookCopy.BookID);
            this.enMode = Mode.Update;
        }

        private bool AddNewBorrowingRecord()
        {
            this.BorrowingRecordID = clsBorrowingRecordDB.AddNewBorrowingRecord(UserID, CopyID, BorrowingDate, DueDate, ActualReturnDate);
            return this.BorrowingRecordID != -1;
        }

        public static DataTable GetBorrowingBooksCount()
        {
            return clsBorrowingRecordDB.GetBorrowingBooksCount();
        }

        private bool UpdateBorrowingRecord()
        {
            return clsBorrowingRecordDB.UpdateBorrowingRecord(BorrowingRecordID, UserID, CopyID, BorrowingDate, DueDate, ActualReturnDate);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    return AddNewBorrowingRecord();
                case Mode.Update:
                    return UpdateBorrowingRecord();
                default:
                    return false;
            }
        }

        public static bool DoesBorrowingRecordExist(int borrowingRecordID)
        {
            return clsBorrowingRecordDB.DoesBorrowingRecordExist(borrowingRecordID);
        }

        public static clsBorrowingRecord GetBorrowingRecordByID(int borrowingRecordID)
        {
            int userID = -1;
            int copyID = -1;
            DateTime borrowingDate = DateTime.Now;
            DateTime dueDate = DateTime.Now;
            DateTime? actualReturnDate = null;

            if (clsBorrowingRecordDB.GetBorrowingRecordByID(borrowingRecordID, ref userID, ref copyID, ref borrowingDate,
                ref dueDate, ref actualReturnDate))
                return new clsBorrowingRecord(borrowingRecordID, userID, copyID, borrowingDate,
                 dueDate, actualReturnDate);

            return null;
        }

        public static DataTable GetAllBorrowingRecords()
        {
            return clsBorrowingRecordDB.GetAllBorrowingRecords();
        }

        public static bool DeleteBorrowingRecord(int borrowingRecordID)
        {
            return clsBorrowingRecordDB.DeleteBorrowingRecord(borrowingRecordID);
        }
    }
}
