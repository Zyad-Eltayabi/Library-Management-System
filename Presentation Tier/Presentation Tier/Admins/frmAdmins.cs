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

namespace Presentation_Tier.Admins
{
    public partial class frmAdmins : Form
    {
        public frmAdmins()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewAdmin frmAddNewAdmin = new frmAddNewAdmin();
            frmAddNewAdmin.ShowDialog();
        }

        private void frmAdmins_Load(object sender, EventArgs e)
        {
            GetAllAdmins();
        }

        private void GetAllAdmins()
        {
            dgvTable.DataSource = clsAdmins.GetAllAdmins();
        }
    }
}
