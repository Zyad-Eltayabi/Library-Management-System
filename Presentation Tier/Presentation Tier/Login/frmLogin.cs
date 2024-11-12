using Application_Tier;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateTextBoxes()
        {
            Guna2TextBox[] textBoxes = { txtUser, txtPass };

            foreach (Guna2TextBox box in textBoxes)
            {
                if (string.IsNullOrEmpty(box.Text))
                {
                    clsUtilityLibrary.PrintWarningMessage("Some fields are required");
                    return false;
                }
            }
            return true;
        }

        private void CheckUsernameAndPassword()
        {
            DataTable adminDetails = clsAdmin.GetAdminByUserNameAndPassword(txtUser.Text.ToString(), txtPass.Text.ToString());

            if (adminDetails.Rows.Count == 0)
            {
                clsUtilityLibrary.PrintErrorMessage("Incorrect UserName or Password.");
                return;
            }

            if (adminDetails.Rows[0]["IsActive"].ToString() == "False")
            {
                clsUtilityLibrary.PrintErrorMessage("Sorry, your account is not active.");
                return;
            }

            frmMain main = new frmMain();
            main.ShowDialog();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes())
                return;

            CheckUsernameAndPassword();
        }
    }
}
