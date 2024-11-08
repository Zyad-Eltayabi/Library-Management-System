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
using static Guna.UI2.Native.WinApi;

namespace Presentation_Tier.Borrowing
{
    public partial class frmBorrowBook : Form
    {
        clsBookCopies _bookCopy { get; set; }
        clsBorrowingRecords _borrowingRecord { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public frmBorrowBook(clsBookCopies bookCopy)
        {
            InitializeComponent();
            _bookCopy = bookCopy;
            enMode = Mode.Add;
        }

        public frmBorrowBook(clsBorrowingRecords borrowingRecord)
        {
            InitializeComponent();
            _borrowingRecord = borrowingRecord;
            enMode = Mode.Update;
        }



        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            SetBookInfo();
        }

        private void SetBookInfo()
        {
            switch (enMode)
            {
                case Mode.Add:
                    SetBookCopyInfo();
                    break;
                case Mode.Update:
                    SetBookCopyInfoInUpdateMode();
                    break;
                default:
                    break;
            }
        }

        private void GetUserIDs()
        {
            DataTable users = clsUsers.GetAllUsers();
            cbUsers.DataSource = users;
            cbUsers.DisplayMember = "UserID";
        }

        private void SetBookCopyInfo()
        {
            if (_bookCopy != null)
            {
                lbCopyID.Text = _bookCopy.CopyID.ToString();
                lbBookTitle.Text = _bookCopy.Book.Title.ToString();
                txtBorrowingDate.Text = DateTime.Now.ToShortDateString();
                dtDueDate.MinDate = DateTime.Now.AddDays(1);
                GetUserIDs();
            }
        }

        private void SetBookCopyInfoInUpdateMode()
        {
            if (_borrowingRecord != null)
            {
                lbBorrowingRecordID.Text = _borrowingRecord.BorrowingRecordID.ToString();
                lbCopyID.Text = _borrowingRecord.CopyID.ToString();
                lbBookTitle.Text = (clsBookCopies.GetBookCopyByID(_borrowingRecord.CopyID)).Book.Title.ToString();
                txtBorrowingDate.Text = _borrowingRecord.BorrowingDate.ToShortDateString();
                dtDueDate.MinDate = _borrowingRecord.DueDate;
                dtActualReturnDate.Enabled = true;
                dtActualReturnDate.MinDate = DateTime.Now;
                if (_borrowingRecord.ActualReturnDate != null)
                {
                    dtActualReturnDate.Value = _borrowingRecord.ActualReturnDate.Value;
                }
                GetUserIDs();
                SetUserID();
                this.Text = "Return The Book";
                btnBorrow.Text = "Save";
            }
        }

        private void SetUserID()
        {
            int indexOfUserIDeInComboBoxAuthor = cbUsers.FindString(_borrowingRecord.UserID.ToString());

            if (indexOfUserIDeInComboBoxAuthor >= 0)
            {
                cbUsers.SelectedIndex = indexOfUserIDeInComboBoxAuthor;
            }
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
                clsUsers user = clsUsers.GetUserByID(userID);
                if (user != null)
                {
                    lbUserName.Text = user.FirstName + " " + user.LastName;
                }
            }
        }

        private void AddNewBorrowingRecord()
        {
            int userID = int.Parse(cbUsers.Text.ToString());
            _borrowingRecord = new clsBorrowingRecords(
                userID,
                _bookCopy.CopyID,
                DateTime.Now,
                dtDueDate.Value,
                null
                );

            // First, inactive a book copy.
            _bookCopy.AvailabilityStatus = false;

            if (_bookCopy.UpdateBookCopy())
            {
                //if book deactivated successfully, then save a new borrowing record
                if (_borrowingRecord.Save())
                {
                    lbBorrowingRecordID.Text = _borrowingRecord.BorrowingRecordID.ToString();
                    clsUtilityLibrary.PrintInfoMessage("Successful operation.");
                    return;
                }
            }

            clsUtilityLibrary.PrintErrorMessage("Failed to save");
        }

        private void UpdateBorrowingRecord()
        {
            _borrowingRecord.DueDate = dtDueDate.Value;
            _borrowingRecord.UserID = int.Parse(cbUsers.Text.ToString());
            if (dtActualReturnDate.Enabled)
                _borrowingRecord.ActualReturnDate = dtActualReturnDate.Value;

            // First, active a book copy.
            _bookCopy = clsBookCopies.GetBookCopyByID(_borrowingRecord.CopyID);
            _bookCopy.AvailabilityStatus = true;

            if (_bookCopy.UpdateBookCopy())
            {
                //if book activated successfully, then update the borrowing record
                if (_borrowingRecord.Save())
                {
                    clsUtilityLibrary.PrintInfoMessage("borrowing record updated successfully.");
                    return;
                }
            }

            clsUtilityLibrary.PrintErrorMessage("Failed to save");

        }

        private void Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    AddNewBorrowingRecord();
                    enMode = Mode.Update;
                    break;
                case Mode.Update:
                    UpdateBorrowingRecord();
                    break;
                default:
                    break;
            }
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
