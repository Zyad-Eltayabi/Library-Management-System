using Presentation_Tier.Admins;
using Presentation_Tier.Authors;
using Presentation_Tier.BookCopies;
using Presentation_Tier.Books;
using Presentation_Tier.Borrowing;
using Presentation_Tier.Dashboard;
using Presentation_Tier.Fines;
using Presentation_Tier.Reservations;
using Presentation_Tier.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewFormToMainPanel(Form form)
        {
            pnMainPanel.Controls.Clear();

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;  // Remove borders to make it look integrated

            pnMainPanel.Controls.Add(form);
            form.Show();

        }

        private void btnAdmins_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmAdmins());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnDashboard.PerformClick();
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageAuthors());
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageUsers());
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageBooks());
        }

        private void btnCopies_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageBookCopies());
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageBorrowingBooks());
        }

        private void btnFines_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageFines());
        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmManageReservations());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmDashboard());
        }
    }
}
