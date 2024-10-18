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

namespace Presentation_Tier.Authors
{
    public partial class frmAddOrUpdateAuthor : Form
    {
        public frmAddOrUpdateAuthor()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtDateOfDeath.Enabled = true;
            }
            else
            {
                dtDateOfDeath.Enabled = false;
            }
        }

        private bool ValidateTextBoxes()
        {
            Guna2TextBox[] guna2TextBox =  { txtFirstName, txtLastName};
            return clsUtilityLibrary.ValidateTextBoxes(guna2TextBox);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes())
                return;
        }
    }
}
