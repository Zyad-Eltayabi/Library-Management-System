using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBookCopy
    {
        public int CopyID { get; set; }
        public int BookID { get; set; }
        public bool AvailabilityStatus { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }
        public clsBook Book { get; set; }

        public clsBookCopy(int bookID, bool availabilityStatus)
        {
            BookID = bookID;
            Book = clsBook.GetBookByID(bookID);
            AvailabilityStatus = availabilityStatus;
            this.enMode = Mode.Add;
        }

        private clsBookCopy(int copyID, int bookID, bool availabilityStatus)
        {
            CopyID = copyID;
            BookID = bookID;
            AvailabilityStatus = availabilityStatus;
            this.enMode = Mode.Update;
            Book = clsBook.GetBookByID(bookID);
        }

        public bool AddBookCopies(int NumberOfCopies)
        {
            for (int i = 0; i < NumberOfCopies; i++)
            {
                this.CopyID = clsBookCopyDB.AddNewBookCopy(BookID, AvailabilityStatus);

                if (CopyID == -1)
                    return false;
            }
            enMode = Mode.Update;
            return true;
        }

        public static DataTable GetAllBookCopies()
        {
            return clsBookCopyDB.GetAllBookCopies();
        }

        public static bool DoesBookCopyExist(int bookCopyID)
        {
            return clsBookCopyDB.DoesBookCopyExist(bookCopyID);
        }

        public static bool DeleteBookCopy(int bookCopyID)
        {
            return clsBookCopyDB.DeleteBookCopy(bookCopyID);
        }

        public static clsBookCopy GetBookCopyByID(int bookCopyID)
        {
            int bookID = -1;
            bool availabilityStatus = false;

            if (clsBookCopyDB.GetBookCopyByID(bookCopyID, ref bookID, ref availabilityStatus))
                return new clsBookCopy(bookCopyID, bookID, availabilityStatus);

            return null;
        }

        public bool UpdateBookCopy()
        {
            return clsBookCopyDB.UpdateBookCopy(CopyID, BookID, AvailabilityStatus);
        }
    }
}
