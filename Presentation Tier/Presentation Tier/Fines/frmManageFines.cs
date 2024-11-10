﻿using Application_Tier;
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
    public partial class frmManageFines : Form
    {
        public frmManageFines()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            clsBorrowingRecords borrowingRecord = clsBorrowingRecords.GetBorrowingRecordByID(1);
            if (borrowingRecord != null)
            {
                frmAddNewFine addNewFine = new frmAddNewFine(borrowingRecord);
                addNewFine.ShowDialog();    
            }
        }
    }
}
