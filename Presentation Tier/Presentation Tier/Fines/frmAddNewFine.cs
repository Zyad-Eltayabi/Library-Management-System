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
        private clsBorrowingRecord _borrowingRecord { get; set; }
        private clsFine _fine { get; set; }

        public frmAddNewFine(clsBorrowingRecord borrowingRecord)
        {
            InitializeComponent();
            _borrowingRecord = borrowingRecord;
            enMode = Mode.Add;
        }

        public frmAddNewFine(clsFine fine)
        {
            InitializeComponent();
            _fine = fine;
            enMode = Mode.Update;
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
                    SetFineInfoInUpdateMode();
                    break;
                default:
                    break;
            }
        }

        private void SetFineInfoInUpdateMode()
        {
            if (_fine != null)
            {
                this.Text = "Update Fine";
                lbFineID.Text = _fine.FineID.ToString();
                lbBorrowingRecordID.Text = _fine.BorrowingRecordID.ToString();
                lbCopyID.Text = _fine.BorrowingRecord.CopyID.ToString();
                lbBookTitle.Text = _fine.BorrowingRecord.Book.Title.ToString();
                lbUserID.Text = _fine.BorrowingRecord.UserID.ToString();
                lbUserName.Text = (_fine.BorrowingRecord.User.FirstName + " " + _fine.BorrowingRecord.User.LastName).ToString();
                lbLateDays.Text = _fine.NumberOfLateDays.ToString();
                lbDefaultFinePerDay.Text = GetDefaultFinePerDay().ToString();
                lbFineAmount.Text = _fine.FineAmount.ToString();
                cbPaymentStatus.Checked = _fine.PaymentStatus;
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

        private void AddNewFine()
        {
            _fine = new clsFine(
                _borrowingRecord.UserID,
                _borrowingRecord.BorrowingRecordID,
                (short)GetNumberOfLateDays(),
                (decimal)GetFineAmount(),
                cbPaymentStatus.Checked
                );

            if (_fine.Save())
            {
                clsUtilityLibrary.PrintInfoMessage("Data Saved Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed to save");
            }
        }

        private void UpdateFine()
        {
            _fine.PaymentStatus = cbPaymentStatus.Checked;
            if (_fine.Save())
            {
                clsUtilityLibrary.PrintInfoMessage("Data updated Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed to save");
            }
        }

        private void Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    enMode = Mode.Update;
                    AddNewFine();
                    break;
                case Mode.Update:
                    UpdateFine();
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
