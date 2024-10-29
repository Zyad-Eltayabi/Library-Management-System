using Database_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBooks
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string AdditionalDetails { get; set; }
        public string BookImage { get; set; }
        public int AuthorID { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsBooks(string title, string iSBN, DateTime publicationDate, string genre, string additionalDetails,
            string bookImage, int authorID)
        {
            Title = title;
            ISBN = iSBN;
            PublicationDate = publicationDate;
            Genre = genre;
            AdditionalDetails = additionalDetails;
            BookImage = bookImage;
            AuthorID = authorID;
            enMode = Mode.Add;
        }

        private bool AddNewBook()
        {
            this.BookID = clsBooksDB.AddNewBook(Title, ISBN, PublicationDate, Genre, AdditionalDetails,
             BookImage, AuthorID);

            return this.BookID != -1;
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    {
                        enMode = Mode.Update;
                        return AddNewBook();
                    }
                case Mode.Update:
                    {
                        break;
                    }
            }
            return false;
        }
    }
}
