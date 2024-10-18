using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Authors
{
    public partial class frmManageAuthors : Form
    {
        public frmManageAuthors()
        {
            InitializeComponent();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddOrUpdateAuthor addOrUpdateAuthor = new frmAddOrUpdateAuthor();
            addOrUpdateAuthor.ShowDialog();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddOrUpdateAuthor addOrUpdateAuthor = new frmAddOrUpdateAuthor();
            addOrUpdateAuthor.ShowDialog();
        }
    }
}
