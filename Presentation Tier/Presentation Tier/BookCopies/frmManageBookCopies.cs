﻿using Application_Tier;
using Presentation_Tier.Books;
using Presentation_Tier.Borrowing;
using Presentation_Tier.Reservations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace Presentation_Tier.BookCopies
{
    public partial class frmManageBookCopies : Form
    {
        public frmManageBookCopies()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewBookCopy addNewBookCopy = new frmAddNewBookCopy();
            addNewBookCopy.ShowDialog();
            GetBookCopies();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewBookCopy addNewBookCopy = new frmAddNewBookCopy();
            addNewBookCopy.ShowDialog();
            GetBookCopies();
        }

        private void frmManageBookCopies_Load(object sender, EventArgs e)
        {
            GetBookCopies();
        }

        private void GetBookCopies()
        {
            dgvTable.DataSource = clsBookCopy.GetAllBookCopies();
            lbRecords.Text = dgvTable.Rows.Count.ToString();
        }

        private int GetBookID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["BookID"].Value.ToString());
        }

        private int GetCopyID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["CopyID"].Value.ToString());
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowBookDetails showBookDetails = new frmShowBookDetails(GetBookID());
            showBookDetails.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }

        private void SetFilter(string colName, string colValue)
        {
            DataTable _bookCopiesTable = clsBookCopy.GetAllBookCopies();
            DataView dv = new DataView();
            dv = _bookCopiesTable.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copyID = GetCopyID();

            if (MessageBox.Show($"Are you sure to delete this Book Copy where ID = {copyID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (clsBookCopy.DoesBookCopyExist(copyID))
                {
                    if (clsBookCopy.DeleteBookCopy(copyID))
                    {
                        clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                        GetBookCopies();
                        return;
                    }
                }

                clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copyID = GetCopyID();
            frmUpdateBookCopy updateBookCopy = new frmUpdateBookCopy(copyID);
            updateBookCopy.ShowDialog();
            GetBookCopies();
        }

        private void borrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copyID = GetCopyID();
            clsBookCopy bookCopy = clsBookCopy.GetBookCopyByID(copyID);

            if (bookCopy == null)
            {
                clsUtilityLibrary.PrintWarningMessage("This book is not found");
                return;
            }

            if (!bookCopy.AvailabilityStatus)
            {
                clsUtilityLibrary.PrintWarningMessage("This book is already borrowed, choose another book.");
                return;
            }

            frmBorrowBook frmBorrowBook = new frmBorrowBook(bookCopy);
            frmBorrowBook.ShowDialog();
            GetBookCopies();
        }



        private void CheckIsThereAnyReservationOnThisBookCopy(ref clsBookCopy bookCopy)
        {
            int getLatestReservationIdOnBookCopy = clsReservation.GetLatestReservationIdOnBookCopy(bookCopy.CopyID);
            if (getLatestReservationIdOnBookCopy == -1)
            {
                // That is mean there is not any reservation on this book copy and available to reserve
                frmCreateNewReservation createNewReservation = new frmCreateNewReservation(bookCopy.CopyID);
                createNewReservation.ShowDialog();
                GetBookCopies();
                return;
            }

            // check The book copy Is borrowed and is returned or not.
            clsReservation reservation = clsReservation.GetReservationRecordByID(getLatestReservationIdOnBookCopy);
            if (reservation.IsBorrowed && reservation.IsReturned)
            {
                // That mean this book copy is available to reserve
                frmCreateNewReservation createNewReservation = new frmCreateNewReservation(bookCopy.CopyID);
                createNewReservation.ShowDialog();
                GetBookCopies();
                return;
            }

            if(!reservation.IsBorrowed)
            {
                clsUtilityLibrary.PrintWarningMessage("We cannot reserve this book because there was a reservation on it and did not borrowed yet to the user.");
                return;
            }

            if(!reservation.IsReturned)
            {
                clsUtilityLibrary.PrintWarningMessage("We cannot reserve this book because there was a reservation on it and did not returned yet from the user.");
                return;
            }
        }

        private void IsBookAvailableForBorrowing()
        {
            int copyID = GetCopyID();
            clsBookCopy bookCopy = clsBookCopy.GetBookCopyByID(copyID);

            if (bookCopy == null)
            {
                clsUtilityLibrary.PrintWarningMessage("This book is not found");
                return;
            }

            if (bookCopy.AvailabilityStatus)
            {
                clsUtilityLibrary.PrintWarningMessage("This book is available to borrow !!");
                return;
            }

            CheckIsThereAnyReservationOnThisBookCopy(ref bookCopy);
        }

        private void ReserveBookCopy()
        {
            IsBookAvailableForBorrowing();
        }

        private void reserveTheBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReserveBookCopy();
        }
    }
}
