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
        public frmBorrowBook(int copyID)
        {
            InitializeComponent();
            _bookCopy = clsBookCopies.GetBookCopyByID(copyID);
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
            //cbUsers.SelectedIndex = -1;
        }

        private void SetBookCopyInfo()
        {
            if (_bookCopy != null)
            {
                lbCopyID.Text = _bookCopy.CopyID.ToString();
                lbBookTitle.Text = _bookCopy.Book.Title.ToString();
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

        
    }
}
