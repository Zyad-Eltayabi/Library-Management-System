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
using System.Xml.Linq;

namespace Presentation_Tier.Users
{
    public partial class frmAddNewUser : Form
    {
        private enum Mode { Add = 1, Update = 2 };
        private Mode _enMode;
        int _userID;
        clsUsers _updatedUser { get; set; }

        public frmAddNewUser()
        {
            InitializeComponent();
            _enMode = Mode.Add;
        }

        public frmAddNewUser(int userID)
        {
            InitializeComponent();
            _enMode = Mode.Update;
            _userID = userID;

        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            GetAllCountries();
            if (_enMode == Mode.Update)
                SetUserInfo();

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
            Guna2TextBox[] textBoxes = { txtFirstName, txtLastName, txtEmail, txtPhoneNumber, txtAddress };
            return clsUtilityLibrary.ValidateTextBoxes(textBoxes);
        }

        private void AddNewUser()
        {
            string newLibraryCard = (Guid.NewGuid()).ToString();
            int nationalityNumber = cbCountry.SelectedIndex + 1;

            clsUsers user = new clsUsers(
                newLibraryCard,
                txtFirstName.Text,
                txtLastName.Text,
                dtDateOfBirth.Value,
                rbMale.Checked,
                txtEmail.Text,
                txtPhoneNumber.Text,
                txtAddress.Text,
                DateTime.Now,
                nationalityNumber,
                clsUsers.Mode.Add
                );

            if (user.Save())
            {
                lbUserID.Text = user.UserID.ToString();
                this.Text = "Update User";
                clsUtilityLibrary.PrintInfoMessage("Data Saved Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Sorry, Failed To Save");
            }
        }

        private void Save()
        {
            switch (_enMode)
            {
                case Mode.Add:
                    _enMode = Mode.Update;
                    AddNewUser();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes())
                return;

            Save();
        }

        private void SetUserInfo()
        {
            this.Text = "Update User";

            _updatedUser = clsUsers.GetUserByID(_userID);

            lbUserID.Text = _updatedUser.UserID.ToString();
            txtFirstName.Text = _updatedUser.FirstName.ToString();
            txtLastName.Text = _updatedUser.LastName.ToString();
            dtDateOfBirth.Value = _updatedUser.DateOfBirth;

            if (_updatedUser.Gender)
                rbMale.Checked = true;
            else rbFemale.Checked = true;

            txtEmail.Text = _updatedUser.Email.ToString();
            txtPhoneNumber.Text = _updatedUser.PhoneNumber.ToString();
            txtAddress.Text = _updatedUser.Address.ToString();

            cbCountry.SelectedIndex = _updatedUser.NationalityID - 1;
        }
    }
}
