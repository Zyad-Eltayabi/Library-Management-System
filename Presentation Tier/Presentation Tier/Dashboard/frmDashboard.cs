using Application_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Dashboard
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            GetStatistics();
        }

        private void GetStatistics()
        {
            try
            {
                GetTotalBooks();
                GetBookCopiesCount();
                GetAuthorsCount();
                GetUsersCount();
                GetAdminsCount();
                GetBorrowingBooksCount();
                GetReservationsCount();
            }
            catch (Exception ex)
            {
                clsUtilityLibrary.PrintWarningMessage(ex.Message);
            }
        }

        private void GetTotalBooks()
        {
            DataTable dt = clsBook.GetBooksCount();
            if (dt != null)
            {
                lbTotalBooks.Text = dt.Rows[0][0].ToString();
            }
        }

        private void GetBookCopiesCount()
        {
            DataTable dt = clsBookCopy.GetBookCopiesCount();
            if (dt.Rows.Count > 0)
                lbBookCopies.Text = dt.Rows[0][0].ToString();
        }

        private void GetAuthorsCount()
        {
            DataTable dt = clsAuthor.GetAuthorsCount();
            if (dt.Rows.Count > 0)
                lbTotalAuthors.Text = dt.Rows[0][0].ToString();
        }

        private void GetUsersCount()
        {
            DataTable dt = clsUser.GetUsersCount();
            if (dt.Rows.Count > 0)
                lbTotalUsers.Text = dt.Rows[0][0].ToString();
        }

        private void GetAdminsCount()
        {
            DataTable dt = clsAdmin.GetAdminsCount();
            if (dt.Rows.Count > 0)
                lbTotalAdmins.Text = dt.Rows[0][0].ToString();
        }

        private void GetBorrowingBooksCount()
        {
            DataTable dt = clsBorrowingRecord.GetBorrowingBooksCount();
            if (dt.Rows.Count > 0)
                lbBorrowingBooks.Text =dt.Rows[0][0].ToString();
        }

        private void GetReservationsCount()
        {
            DataTable dt = clsReservation.GetReservationsCount();
            if (dt.Rows.Count > 0)
                lbTotalReservations.Text = dt.Rows[0][0].ToString();
        }
    }
}
