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

namespace Presentation_Tier.BookCopies
{
    public partial class frmUpdateBookCopy : Form
    {
        private clsBookCopies _bookCopy;
        private int _copyID;

        public frmUpdateBookCopy(int copyID)
        {
            InitializeComponent();
            _copyID = copyID;
        }

        private void frmUpdateBookCopy_Load(object sender, EventArgs e)
        {
            GetBookInfo();
        }

        private void GetBookInfo()
        {
            if (clsBookCopies.DoesBookCopyExist(_copyID))
            {
                _bookCopy = clsBookCopies.GetBookCopyByID(_copyID);
                if (_bookCopy != null)
                {
                    lbCopyID.Text = _bookCopy.CopyID.ToString();
                    lbBookID.Text = _bookCopy.BookID.ToString();
                    cbAVA.Checked = _bookCopy.AvailabilityStatus;
                    return;
                }
            }

            clsUtilityLibrary.PrintErrorMessage("We can not upload the book at this time.");
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateBookCopy();
        }

        private void UpdateBookCopy()
        {
            if (_bookCopy != null)
            {
                _bookCopy.AvailabilityStatus = cbAVA.Checked;
                if (_bookCopy.UpdateBookCopy())
                {
                    clsUtilityLibrary.PrintInfoMessage("Book copy updated successfully.");
                    return;
                }
            }

            clsUtilityLibrary.PrintErrorMessage("Book copy does not updated successfully.");
        }
    }
}
