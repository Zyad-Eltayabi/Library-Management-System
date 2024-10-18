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

        public frmAddOrUpdateAuthor()
        {
            InitializeComponent();
            _enMode = Mode.Add;
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

            clsAuthors author = new clsAuthors(
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

        private void Save()
        {
            switch (_enMode)
            {
                case Mode.Add:
                    _enMode = Mode.Update;
                    AddNewAuthor();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void frmAddOrUpdateAuthor_Load(object sender, EventArgs e)
        {
            GetAllCountries();
        }

        private void GetAllCountries()
        {
            DataTable countries = clsCountries.GetAllCountries();
            cbCountry.DataSource = countries;
            cbCountry.ValueMember = "CountryName";
        }
    }
}
