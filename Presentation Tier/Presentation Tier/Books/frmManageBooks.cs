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

namespace Presentation_Tier.Books
{
    public partial class frmManageBooks : Form
    {
        public frmManageBooks()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateBook addAndUpdateBook = new frmAddAndUpdateBook();
            addAndUpdateBook.ShowDialog();
            GetAllBooks();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateBook addAndUpdateBook = new frmAddAndUpdateBook();
            addAndUpdateBook.ShowDialog();
            GetAllBooks();
        }

        private void frmManageBooks_Load(object sender, EventArgs e)
        {
            GetAllBooks();
        }

        private void GetAllBooks()
        {
            dgvTable.DataSource = clsBooks.GetAllBooks();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private int GetBookID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["BookID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = GetBookID();

            if (clsBooks.DoesBookExist(bookID))
            {
                frmAddAndUpdateBook updateBook = new frmAddAndUpdateBook(bookID);
                updateBook.ShowDialog();
            }
            else
            {
                clsUtilityLibrary.PrintWarningMessage("this book is not found.");
            }

            GetAllBooks();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bookID = GetBookID();

            if (MessageBox.Show($"Are you sure to delete this Book where ID = {bookID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (clsBooks.DoesBookExist(bookID))
                {
                    if (clsBooks.DeleteBook(bookID))
                    {
                        clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                        GetAllBooks();
                        return;
                    }
                }

                clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
            }
        }
    }
}
