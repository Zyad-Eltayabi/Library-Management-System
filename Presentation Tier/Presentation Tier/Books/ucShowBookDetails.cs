using Application_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Books
{
    public partial class ucShowBookDetails : UserControl
    {
        clsBook _book { get; set; }
        public ucShowBookDetails()
        {
            InitializeComponent();
        }

        public void LoadBookInfo(int bookId)
        {
            if (clsBook.DoesBookExist(bookId))
            {
                _book = clsBook.GetBookByID(bookId);
                lbBookID.Text = _book.BookID.ToString();
                lbGenre.Text = _book.Genre.ToString();
                lbISBN.Text = _book.ISBN.ToString();
                lbTitle.Text = _book.Title.ToString();
                lbAuthor.Text = _book.Author.AuthorFullName();
                lbDate.Text = _book.PublicationDate.ToString();
                txtDetails.Text = _book.AdditionalDetails.ToString();
                SetImage();
            }
            else
            {
                clsUtilityLibrary.PrintWarningMessage("This book is not found");
            }
        }

        private string GetBooksImagesFolderPath()
        {
            string booksImagesPath = $@"{Application.StartupPath}\Book_Images";
            if (!Directory.Exists(booksImagesPath))
                Directory.CreateDirectory($@"{Application.StartupPath}\Book_Images");

            return booksImagesPath;
        }

        private void SetImage()
        {
            if (!string.IsNullOrEmpty(_book.BookImage))
            {
                string path = $@"{GetBooksImagesFolderPath()}\{_book.BookImage}";
                if (File.Exists(path))
                {
                    picBook.Image = Image.FromFile(path);
                }
            }
        }

    }
}
