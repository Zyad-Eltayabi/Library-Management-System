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
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateBook addAndUpdateBook = new frmAddAndUpdateBook();
            addAndUpdateBook.ShowDialog();
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
        }
    }
}
