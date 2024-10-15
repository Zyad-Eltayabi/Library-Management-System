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
    public partial class frmAddNewAdmin : Form
    {
        public frmAddNewAdmin()
        {
            InitializeComponent();
            _enMode = Mode.Add;
        }

        private enum Mode { Add = 1, Update = 2 };
        private Mode _enMode;

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtUserName, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtPass, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPass, "");
            }
        }

        private bool ValidateCheckBox()
        {
            return string.IsNullOrEmpty(txtUserName.Text.ToString()) || string.IsNullOrEmpty(txtPass.Text.ToString());
        }

        private void AddNewAdmin()
        {
            clsAdmins newAdmin = new clsAdmins(txtName.Text.ToString(), txtUserName.Text.ToString(),
                txtPass.Text.ToString(), cbIsActive.Checked);

            if (newAdmin.Save())
            {
                clsUtilityLibrary.PrintInfoMessage("Data Saved Successfully.");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("failed to save");
            }
        }

        private void AddOrUpdateAdmin()
        {
            switch (_enMode)
            {
                case Mode.Add:
                    {
                        _enMode = Mode.Update;
                        AddNewAdmin();
                        break;
                    }
                case Mode.Update:
                    {
                        break;
                    }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ValidateCheckBox())
            {
                MessageBox.Show("Some fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddOrUpdateAdmin();
        }
    }
}
