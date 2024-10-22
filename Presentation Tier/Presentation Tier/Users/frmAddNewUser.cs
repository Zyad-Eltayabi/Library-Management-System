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
using System.Xml.Linq;

namespace Presentation_Tier.Users
{
    public partial class frmAddNewUser : Form
    {
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            GetAllCountries();
        }

        private void GetAllCountries()
        {
            DataTable countries = clsCountries.GetAllCountries();
            cbCountry.DataSource = countries;
            cbCountry.ValueMember = "CountryName";
        }

        private void ValidateTextBox(Guna2TextBox textBox, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                errorProvider1.SetError(textBox, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, "");
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(txtFirstName, e);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(txtEmail, e);
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(txtLastName, e);
        }

        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(txtPhoneNumber, e);
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(txtAddress, e);
        }

        private bool ValidateTextBoxes()
        {
            Guna2TextBox[] textBoxes =  { txtFirstName,txtLastName,txtEmail,txtPhoneNumber,txtAddress};
            return clsUtilityLibrary.ValidateTextBoxes(textBoxes);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes())
                return;
        }
    }
}
