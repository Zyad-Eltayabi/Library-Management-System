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
            dgvTable.DataSource = clsUser.GetAllUsers();
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

        private int GetUserID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["UserID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = GetUserID();

            if (clsUser.DoesUserExist(userID))
            {
                frmAddNewUser addNewUser = new frmAddNewUser(userID);
                addNewUser.ShowDialog();
                GetAllUsers();
                return;
            }

            clsUtilityLibrary.PrintErrorMessage("Sorry, This user is not exist");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = GetUserID();

            if (clsUser.DoesUserExist(userID))
            {
                if (MessageBox.Show($"Are you sure to delete this User where ID = {userID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (clsUser.DeleteUser(userID))
                    {
                        clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                        GetAllUsers();
                    }
                    else
                    {
                        clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
                    }
                }
                return;
            }

            clsUtilityLibrary.PrintErrorMessage("Sorry, This user is not exist");
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (clsUser.DoesUserExist(GetUserID()))
            {
                frmShowDetails showDetails = new frmShowDetails(GetUserID());
                showDetails.ShowDialog();
                return;
            }

            clsUtilityLibrary.PrintErrorMessage("Sorry, This user is not exist");
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }

        private void SetFilter(string colName, string colValue)
        {
            DataTable usersTable = clsUser.GetAllUsers();
            DataView dv = new DataView();
            dv = usersTable.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }

    }
}
