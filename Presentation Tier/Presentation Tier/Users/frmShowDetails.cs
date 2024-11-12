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
    public partial class frmShowDetails : Form
    {
        int _userID { get; set; }
        public frmShowDetails(int userID)
        {
            InitializeComponent();
            this._userID = userID;
        }

        private void frmShowDetails_Load(object sender, EventArgs e)
        {
            SetUserDetails();
        }

        private void SetUserDetails()
        {
            clsUser user = clsUser.GetUserByID(_userID);

            lbUserID.Text = user.UserID.ToString();
            lbUserName.Text = user.FirstName;
            lbLastName.Text = user.LastName;
            lbEmail.Text = user.Email;
            lbAddress.Text = user.Address;
            lbTel.Text = user.PhoneNumber;
            dtDateOfBirth.Value = user.DateOfBirth;

            if (user.Gender)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            lbCountry.Text = clsCountry.GetCountryName(user.NationalityID);
        }
    }
}
