using Database_Tier;
using System;
using System.Collections.Generic;
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

        public clsBookCopies(int bookID, bool availabilityStatus)
        {
            BookID = bookID;
            AvailabilityStatus = availabilityStatus;
            this.enMode = Mode.Add;
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
    }
}
