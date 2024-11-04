using Application_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.BookCopies
{
    public partial class frmAddNewBookCopy : Form
    {
        public frmAddNewBookCopy()
        {
            InitializeComponent();
        }
        private int _bookID;

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    {
                        if (!char.IsDigit(e.KeyChar))
                            e.Handled = true;
                        break;
                    }
                default:
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            GetBook();
        }

        private void GetBook()
        {
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    GetBookByID();
                    break;
                case 1:
                    GetBookByTitle();
                    break;
                default:
                    break;
            }
        }

        private void GetBookByTitle()
        {
            int bookID = clsBooks.GetBookIDByBookTitle(txtFilter.Text.ToString());

            if (bookID == -1)
            {
                clsUtilityLibrary.PrintWarningMessage($"Sorry, This book is not found");
                btnSave.Enabled = false;
                return;
            }

            ucShowBookDetails1.LoadBookInfo(bookID);
            _bookID = bookID;
            btnSave.Enabled = true;
        }

        private void GetBookByID()
        {
            int bookID = int.Parse(txtFilter.Text.ToString());
            if (clsBooks.DoesBookExist(bookID))
            {
                _bookID = bookID;
                ucShowBookDetails1.LoadBookInfo(bookID);
                btnSave.Enabled = true;
            }
            else
            {
                clsUtilityLibrary.PrintWarningMessage($"Sorry, This book with ID {bookID} is not found");
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddBookCopies();
        }

        private void AddBookCopies()
        {
            int numberOfBookCopies = int.Parse(nudValue.Value.ToString());

            clsBookCopies bookCopies = new clsBookCopies(_bookID, true);

            if (bookCopies.AddBookCopies(numberOfBookCopies))
            {
                clsUtilityLibrary.PrintInfoMessage("Successfully Operation");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed operation to add book copies");
            }
        }
    }
}
