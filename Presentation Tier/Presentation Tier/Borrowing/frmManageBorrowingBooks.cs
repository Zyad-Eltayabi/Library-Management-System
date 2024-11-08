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
    }
}
