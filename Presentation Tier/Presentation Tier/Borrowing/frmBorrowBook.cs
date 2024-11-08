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

namespace Presentation_Tier.Borrowing
{
    public partial class frmBorrowBook : Form
    {
        clsBookCopies _bookCopy { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public frmBorrowBook(int copyID, Mode mode)
        {
            InitializeComponent();
            _bookCopy = clsBookCopies.GetBookCopyByID(copyID);
            enMode = mode;
        }

        private void frmBorrowBook_Load(object sender, EventArgs e)
        {
            SetBookCopyInfo();
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
            clsBorrowingRecords newBorrowingRecord = new clsBorrowingRecords(
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
                if (newBorrowingRecord.Save())
                {
                    lbBorrowingRecordID.Text = newBorrowingRecord.BorrowingRecordID.ToString();
                    clsUtilityLibrary.PrintInfoMessage("Successful operation.");
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
