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
using static System.ComponentModel.Design.ObjectSelectorEditor;

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
            GetAllAdmins();
        }

        private void frmAdmins_Load(object sender, EventArgs e)
        {
            GetAllAdmins();
        }

        private void GetAllAdmins()
        {
            dgvTable.DataSource = clsAdmins.GetAllAdmins();
            lbRecords.Text = dgvTable.Rows.Count.ToString();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewAdmin frmAddNewAdmin = new frmAddNewAdmin();
            frmAddNewAdmin.ShowDialog();
            GetAllAdmins();
        }

        private int GetSelectedRowID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int adminID = GetSelectedRowID();

            if(MessageBox.Show($"Are you sure to delete this Admin where ID = {adminID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if(clsAdmins.DeleteAdmin(adminID))
                {
                    clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                    GetAllAdmins();
                }
                else
                {
                    clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
                }
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheckAdminPassword frmCheckAdminPassword = new frmCheckAdminPassword(GetSelectedRowID());
            frmCheckAdminPassword.ShowDialog();
            GetAllAdmins();
        }
    }
}
