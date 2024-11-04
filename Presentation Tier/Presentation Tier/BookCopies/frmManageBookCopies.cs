﻿using System;
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
    public partial class frmManageBookCopies : Form
    {
        public frmManageBookCopies()
        {
            InitializeComponent();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewBookCopy addNewBookCopy = new frmAddNewBookCopy();
            addNewBookCopy.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewBookCopy addNewBookCopy = new frmAddNewBookCopy();
            addNewBookCopy.ShowDialog();
        }
    }
}