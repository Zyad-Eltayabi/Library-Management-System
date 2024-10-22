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

namespace Presentation_Tier.Users
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewUser frm = new frmAddNewUser();
            frm.ShowDialog();
            GetAllUsers();
        }

        private void GetAllUsers()
        {
            dgvTable.DataSource = clsUsers.GetAllUsers();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            GetAllUsers();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser frm = new frmAddNewUser();
            frm.ShowDialog();
            GetAllUsers();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetAllUsers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetAllUsers();
        }
    }
}
