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

namespace Presentation_Tier.Reservations
{
    public partial class frmCreateNewReservation : Form
    {
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }
        private clsReservation _reservation { get; set; }
        private clsBookCopy _bookCopy { get; set; }

        public frmCreateNewReservation(int bookCopyID)
        {
            InitializeComponent();
            _bookCopy = clsBookCopy.GetBookCopyByID(bookCopyID);
            enMode = Mode.Add;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    AddNewReservation();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void AddNewReservation()
        {
            int userID = int.Parse(cbUsers.Text.ToString());
            _reservation = new clsReservation(userID, _bookCopy.CopyID, DateTime.Now);
            if (_reservation.Save())
            {
                this.Text = "Update Reservation";
                lbReservationID.Text = _reservation.ReservationID.ToString();
                clsUtilityLibrary.PrintInfoMessage("Data saved successfully");
                return;
            }

            clsUtilityLibrary.PrintErrorMessage("Failed To save");
        }

        private void frmCreateNewReservation_Load(object sender, EventArgs e)
        {
            LoadReservationInfo();
        }

        private void LoadReservationInfo()
        {
            switch (enMode)
            {
                case Mode.Add:
                    LoadReservationInfoInAddMode();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void LoadReservationInfoInAddMode()
        {
            if (_bookCopy != null)
            {
                lbCopyID.Text = _bookCopy.CopyID.ToString();
                lbBookTitle.Text = _bookCopy.Book.Title;
                lbBookTitle.Text = _bookCopy.Book.Title;
                GetUserIDs();
                lbReservationDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void GetUserIDs()
        {
            DataTable users = clsUser.GetAllUsers();
            cbUsers.DataSource = users;
            cbUsers.DisplayMember = "UserID";
        }

        private void cbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUserName();
        }

        private void SetUserName()
        {
            int userID = -1;
            if (int.TryParse(cbUsers.Text.ToString(), out userID))
            {
                clsUser user = clsUser.GetUserByID(userID);
                if (user != null)
                {
                    lbUserName.Text = user.FirstName + " " + user.LastName;
                }
            }
        }


    }
}
