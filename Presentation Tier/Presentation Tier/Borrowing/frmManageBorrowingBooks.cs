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

namespace Presentation_Tier.Borrowing
{
    public partial class frmManageBorrowingBooks : Form
    {
        public frmManageBorrowingBooks()
        {
            InitializeComponent();
        }

        private void frmManageBorrowingBooks_Load(object sender, EventArgs e)
        {
            GetBorrowingBooks();
        }

        private void GetBorrowingBooks()
        {
            dgvTable.DataSource = clsBorrowingRecords.GetAllBorrowingRecords();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int borrowingRecordID = GetBorrowingRecordID();
            clsBorrowingRecords borrowingRecord = clsBorrowingRecords.GetBorrowingRecordByID(borrowingRecordID);
            if (borrowingRecord != null)
            {
                frmBorrowBook borrowBook = new frmBorrowBook(borrowingRecord);
                borrowBook.ShowDialog();
                GetBorrowingBooks();
                return;
            }

            clsUtilityLibrary.PrintWarningMessage($"Sorry, this borrowing book with ID = {borrowingRecordID} is not found");
        }

        private int GetBorrowingRecordID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["BorrowingRecordID"].Value.ToString());
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }

        private void SetFilter(string colName, string colValue)
        {
            DataTable _borrowingBooks = clsBorrowingRecords.GetAllBorrowingRecords();
            DataView dv = new DataView();
            dv = _borrowingBooks.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int borrowingRecordID = GetBorrowingRecordID();

            if (MessageBox.Show($"Are you sure to delete this borrowing record where BorrowingID = {borrowingRecordID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (clsBorrowingRecords.DoesBorrowingRecordExist(borrowingRecordID))
                {
                    if (clsBorrowingRecords.DeleteBorrowingRecord(borrowingRecordID))
                    {
                        clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                        GetBorrowingBooks();
                        return;
                    }
                }

                clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
            }
        }
    }
}
