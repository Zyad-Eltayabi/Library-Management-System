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

namespace Presentation_Tier.Authors
{
    public partial class frmAddOrUpdateAuthor : Form
    {
        private enum Mode { Add = 1, Update = 2 };
        private Mode _enMode;
        private int _authorID;
        private clsAuthor _updatedAuthor;

        public frmAddOrUpdateAuthor()
        {
            InitializeComponent();
            _enMode = Mode.Add;
        }
        public frmAddOrUpdateAuthor(int authorID)
        {
            InitializeComponent();
            _enMode = Mode.Update;
            _authorID = authorID;
            _updatedAuthor = clsAuthor.GetAuthorByID(_authorID);
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

            Save();
        }

        private void AddNewAuthor()
        {
            DateTime? dateOfDeath = null;

            if (checkBox1.Checked)
                dateOfDeath = dtDateOfDeath.Value.Date;


            int nationalityID = cbCountry.SelectedIndex + 1;

            clsAuthor author = new clsAuthor(
                txtFirstName.Text.ToString(),
                txtLastName.Text.ToString(),
                dtDateOfBirth.Value.Date,
                dateOfDeath,
                rbMale.Checked,
                nationalityID
                );
                
            if(author.Save())
            {
                lbAuthorID.Text = author.AuthorID.ToString();
                clsUtilityLibrary.PrintInfoMessage("Data Saved Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed To Save");
            }

        }

        private void UpdateAuthorInfo()
        {
            _updatedAuthor.FirstName = txtFirstName.Text;
            _updatedAuthor.LastName = txtLastName.Text;
            _updatedAuthor.DateOfBirth = dtDateOfBirth.Value.Date;

            if(checkBox1.Checked)
                _updatedAuthor.DateOfDeath = dtDateOfDeath.Value.Date;
            else 
                _updatedAuthor.DateOfDeath = null;

            _updatedAuthor.Gender = rbMale.Checked;

            _updatedAuthor.NationalityID = cbCountry.SelectedIndex + 1;
        }

        private void UpdateAuthor()
        {
            UpdateAuthorInfo();

            if (_updatedAuthor.Save())
            {
                lbAuthorID.Text = _updatedAuthor.AuthorID.ToString();
                clsUtilityLibrary.PrintInfoMessage("Data Saved Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed To Save");
            }
        }

        private void Save()
        {
            switch (_enMode)
            {
                case Mode.Add:
                    _enMode = Mode.Update;
                    AddNewAuthor();
                    break;
                case Mode.Update:
                    UpdateAuthor();
                    break;
                default:
                    break;
            }
        }

        private void frmAddOrUpdateAuthor_Load(object sender, EventArgs e)
        {
            GetAllCountries();
            if(_enMode == Mode.Update)
            {
                SetAuthorInfo();
            }
        }

        private void GetAllCountries()
        {
            DataTable countries = clsCountry.GetAllCountries();
            cbCountry.DataSource = countries;
            cbCountry.ValueMember = "CountryName";
        }

        private void SetAuthorInfo()
        {
            lbAuthorID.Text = _updatedAuthor.AuthorID.ToString();
            txtFirstName.Text = _updatedAuthor.FirstName;
            txtLastName.Text = _updatedAuthor.LastName;
            dtDateOfBirth.Value = _updatedAuthor.DateOfBirth;

            if(_updatedAuthor.DateOfDeath != null)
            {
                checkBox1.Checked = true;
                dtDateOfDeath.Value = _updatedAuthor.DateOfDeath.Value;
            }

            if (_updatedAuthor.Gender)
                rbMale.Checked = true;
            else 
                rbFemale.Checked = true;

            cbCountry.SelectedIndex = _updatedAuthor.NationalityID - 1;
        }
    }
}
