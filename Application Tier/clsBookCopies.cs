using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBookCopies
    {
        public int CopyID { get; set; }
        public int BookID { get; set; }
        public bool AvailabilityStatus { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }
        public clsBooks Book { get; set; }

        public clsBookCopies(int bookID, bool availabilityStatus)
        {
            BookID = bookID;
            Book = clsBooks.GetBookByID(bookID);
            AvailabilityStatus = availabilityStatus;
            this.enMode = Mode.Add;
        }

        private clsBookCopies(int copyID, int bookID, bool availabilityStatus)
        {
            CopyID = copyID;
            BookID = bookID;
            AvailabilityStatus = availabilityStatus;
            this.enMode = Mode.Update;
            Book = clsBooks.GetBookByID(bookID);
        }

        public bool AddBookCopies(int NumberOfCopies)
        {
            for (int i = 0; i < NumberOfCopies; i++)
            {
                this.CopyID = clsBookCopiesDB.AddNewBookCopy(BookID, AvailabilityStatus);

                if (CopyID == -1)
                    return false;
            }
            enMode = Mode.Update;
            return true;
        }

        public static DataTable GetAllBookCopies()
        {
            return clsBookCopiesDB.GetAllBookCopies();
        }

        public static bool DoesBookCopyExist(int bookCopyID)
        {
            return clsBookCopiesDB.DoesBookCopyExist(bookCopyID);
        }

        public static bool DeleteBookCopy(int bookCopyID)
        {
            return clsBookCopiesDB.DeleteBookCopy(bookCopyID);
        }

        public static clsBookCopies GetBookCopyByID(int bookCopyID)
        {
            int bookID = -1;
            bool availabilityStatus = false;

            if (clsBookCopiesDB.GetBookCopyByID(bookCopyID, ref bookID, ref availabilityStatus))
                return new clsBookCopies(bookCopyID, bookID, availabilityStatus);

            return null;
        }

        public bool UpdateBookCopy()
        {
            return clsBookCopiesDB.UpdateBookCopy(CopyID, BookID, AvailabilityStatus);
        }
    }
}
