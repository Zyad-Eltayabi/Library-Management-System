using Application_Tier;
using Presentation_Tier.Books;
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
            dgvTable.DataSource = clsBookCopies.GetAllBookCopies();
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
            DataTable _bookCopiesTable = clsBookCopies.GetAllBookCopies();
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
                if (clsBookCopies.DoesBookCopyExist(copyID))
                {
                    if (clsBookCopies.DeleteBookCopy(copyID))
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
    }
}
