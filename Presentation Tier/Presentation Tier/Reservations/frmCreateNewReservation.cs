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

        public frmCreateNewReservation(clsReservation reservation)
        {
            InitializeComponent();
            _reservation = reservation;
            enMode = Mode.Update;
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
                    UpdateReservation();
                    break;
                default:
                    break;
            }
        }

        private bool ActivateTheBookCopyForBorrowing()
        {
            _reservation.bookCopy.AvailabilityStatus = true;
            if (_reservation.bookCopy.UpdateBookCopy())
                return true;
            return false;
        }

        private void UpdateReservation()
        {
            _reservation.UserID = int.Parse(cbUsers.Text.ToString());
            _reservation.IsBorrowed = cbIsBorrowed.Checked;
            _reservation.IsReturned = cbIsReturned.Checked;

            // Save Updates
            if (_reservation.Save())
            {
                if (_reservation.IsBorrowed && _reservation.IsReturned)
                {
                    if (ActivateTheBookCopyForBorrowing())
                    {
                        clsUtilityLibrary.PrintInfoMessage("Reservation record updated successfully.");
                        return;
                    }
                    else
                    {
                        clsUtilityLibrary.PrintErrorMessage("Failed to update book copy");
                        return;
                    }
                }
                else
                {
                    clsUtilityLibrary.PrintInfoMessage("Reservation record updated successfully.");
                    return;
                }
            }

            clsUtilityLibrary.PrintErrorMessage("Failed to update");
        }

        private void AddNewReservation()
        {
            int userID = int.Parse(cbUsers.Text.ToString());
            _reservation = new clsReservation(userID, _bookCopy.CopyID, DateTime.Now, cbIsBorrowed.Checked, cbIsReturned.Checked);
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
                    LoadReservationInfoInUpdateMode();
                    break;
                default:
                    break;
            }
        }

        private void LoadReservationInfoInUpdateMode()
        {
            if (_reservation != null)
            {
                lbReservationID.Text = _reservation.ReservationID.ToString();
                lbCopyID.Text = _reservation.CopyID.ToString();
                lbBookTitle.Text = _reservation.bookCopy.Book.Title;
                GetUserIDs();
                SetUserID();
                lbReservationDate.Text = _reservation.ReservationDate.ToShortDateString();
                cbIsBorrowed.Checked = _reservation.IsBorrowed;
                cbIsReturned.Checked = _reservation.IsReturned;
            }
        }

        private void SetUserID()
        {
            int indexOfUserIDeInComboBox = cbUsers.FindString(_reservation.UserID.ToString());

            if (indexOfUserIDeInComboBox >= 0)
            {
                cbUsers.SelectedIndex = indexOfUserIDeInComboBox;
            }
        }

        private void LoadReservationInfoInAddMode()
        {
            if (_bookCopy != null)
            {
                lbCopyID.Text = _bookCopy.CopyID.ToString();
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
