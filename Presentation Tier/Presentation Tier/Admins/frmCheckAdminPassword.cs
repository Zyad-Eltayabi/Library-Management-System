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
    public partial class frmCheckAdminPassword : Form
    {
        clsAdmin _admin { get; set; }
        int _adminID { get; set; }
        public frmCheckAdminPassword(int adminID)
        {
            InitializeComponent();
            _admin = clsAdmin.GetAdminByID(adminID);
            _adminID = adminID;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            // encrypt user input in text box and then compare it to the password  in _admin object
            string encryptedUserInput = clsHashing.ComputeHash(txtPass.Text.ToString());

            /* compare the two passwords */

            if (!string.Equals(_admin.Password, encryptedUserInput))
            {
                clsUtilityLibrary.PrintErrorMessage("Error Password.");
                return;
            }

            // if two passwords equals , then open AddOrUpdateAdmin form
            frmAddNewAdmin frmAddNewAdmin = new frmAddNewAdmin(this._adminID);
            frmAddNewAdmin.ShowDialog();
        }
    }
}
