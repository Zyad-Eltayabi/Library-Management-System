using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Books
{
    public partial class frmManageBooks : Form
    {
        public frmManageBooks()
        {
            InitializeComponent(); 
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddAndUpdateBook addAndUpdateBook = new frmAddAndUpdateBook();
            addAndUpdateBook.ShowDialog();
        }
    }
}
