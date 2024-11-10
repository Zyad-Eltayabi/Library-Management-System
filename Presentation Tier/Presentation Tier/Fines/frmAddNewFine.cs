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

namespace Presentation_Tier.Fines
{
    public partial class frmAddNewFine : Form
    {
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }
        private clsBorrowingRecords _borrowingRecord { get; set; }
        public frmAddNewFine(clsBorrowingRecords borrowingRecord)
        {
            InitializeComponent();
            _borrowingRecord = borrowingRecord;
            enMode = Mode.Add;
        }

        private void frmAddNewFine_Load(object sender, EventArgs e)
        {
            SetFineInfo();
        }

        private void SetFineInfo()
        {
            switch (enMode)
            {
                case Mode.Add:
                    SetFineInfoInAddMode();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void SetFineInfoInAddMode()
        {
            if (_borrowingRecord != null)
            {
                lbBorrowingRecordID.Text = _borrowingRecord.BorrowingRecordID.ToString();
                lbCopyID.Text = _borrowingRecord.CopyID.ToString();
                lbBookTitle.Text = _borrowingRecord.Book.Title.ToString();
                lbUserID.Text = _borrowingRecord.UserID.ToString();
                lbUserName.Text = (_borrowingRecord.User.FirstName + " " + _borrowingRecord.User.LastName).ToString();
                lbLateDays.Text = GetNumberOfLateDays().ToString();
                lbDefaultFinePerDay.Text = GetDefaultFinePerDay().ToString();
                lbFineAmount.Text = GetFineAmount().ToString();
            }
        }

        private int GetNumberOfLateDays()
        {
            int number = int.Parse((_borrowingRecord.ActualReturnDate.Value.Day - _borrowingRecord.DueDate.Day).ToString());
            return number;
        }

        private int GetFineAmount()
        {
            return GetNumberOfLateDays() * GetDefaultFinePerDay();
        }

        private int GetDefaultFinePerDay()
        {
            int fine = clsSettings.GetDefaultFinePerDay();
            return fine > -1 ? fine : 0;
        }
    }
}
