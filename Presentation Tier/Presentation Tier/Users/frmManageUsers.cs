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

        private int GetUserID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["UserID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = GetUserID();

            if (clsUsers.DoesUserExist(userID))
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

            if (clsUsers.DoesUserExist(userID))
            {
                if (MessageBox.Show($"Are you sure to delete this User where ID = {userID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (clsUsers.DeleteUser(userID))
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
    }
}
