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
    public partial class frmShowBookDetails : Form
    {
        private int bookID;
        public frmShowBookDetails(int bookID)
        {
            InitializeComponent();
            this.bookID = bookID;
        }

        private void frmShowBookDetails_Load(object sender, EventArgs e)
        {
            ucShowBookDetails1.LoadBookInfo(bookID);
        }
    }
}
