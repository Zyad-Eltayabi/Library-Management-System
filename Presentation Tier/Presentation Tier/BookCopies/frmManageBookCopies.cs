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

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowBookDetails showBookDetails = new frmShowBookDetails(GetBookID());
            showBookDetails.ShowDialog();
        }
    }
}
