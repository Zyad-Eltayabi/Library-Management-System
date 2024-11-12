using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsBook
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
        public string AdditionalDetails { get; set; }
        public string BookImage { get; set; }
        public int AuthorID { get; set; }
        public clsAuthor Author { get; set; }

        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsBook(string title, string iSBN, DateTime publicationDate, string genre, string additionalDetails,
            string bookImage, int authorID)
        {
            Title = title;
            ISBN = iSBN;
            PublicationDate = publicationDate;
            Genre = genre;
            AdditionalDetails = additionalDetails;
            BookImage = bookImage;
            AuthorID = authorID;
            Author = clsAuthor.GetAuthorByID(authorID);
            enMode = Mode.Add;
        }

        private clsBook(int bookID, string title, string iSBN, DateTime publicationDate, string genre, string additionalDetails,
            string bookImage, int authorID)
        {
            BookID = bookID;
            Title = title;
            ISBN = iSBN;
            PublicationDate = publicationDate;
            Genre = genre;
            AdditionalDetails = additionalDetails;
            BookImage = bookImage;
            AuthorID = authorID;
            Author = clsAuthor.GetAuthorByID(authorID);
            this.enMode = Mode.Update;
        }

        private bool AddNewBook()
        {
            this.BookID = clsBooksDB.AddNewBook(Title, ISBN, PublicationDate, Genre, AdditionalDetails,
             BookImage, AuthorID);

            return this.BookID != -1;
        }

        private bool UpdateBook()
        {
            return clsBooksDB.UpdateBook(BookID, Title, ISBN, PublicationDate, Genre,
             AdditionalDetails, BookImage, AuthorID);
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
                        return UpdateBook();
                    }
            }
            return false;
        }

        public static DataTable GetAllBooks()
        {
            return clsBooksDB.GetAllBooks();
        }

        public static bool DoesBookExist(int bookID)
        {
            return clsBooksDB.DoesBookExist(bookID);
        }

        public static clsBook GetBookByID(int bookID)
        {
            string title = "";
            string iSBN = "";
            DateTime publicationDate = DateTime.Now;
            string genre = "";
            string additionalDetails = "";
            string bookImage = "";
            int authorID = -1;


            if (clsBooksDB.GetBookByID(bookID, ref title, ref iSBN, ref publicationDate, ref genre,
            ref additionalDetails, ref bookImage, ref authorID))
                return new clsBook(bookID, title, iSBN, publicationDate, genre,
             additionalDetails, bookImage, authorID);

            return null;
        }

        public static bool DeleteBook(int bookID)
        {
            return clsBooksDB.DeleteBook(bookID);
        }

        public static int GetBookIDByBookTitle(string bookTitle)
        {
            return clsBooksDB.GetBookIDByBookTitle(bookTitle);
        }
    }
}
