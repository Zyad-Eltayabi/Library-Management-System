﻿using Application_Tier;
using Presentation_Tier.Borrowing;
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

        private void frmManageFines_Load(object sender, EventArgs e)
        {
            GetFines();
        }

        private void GetFines()
        {
            dgvTable.DataSource = clsFine.GetAllFines();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private int GetFineID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["FineID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            clsFine fine = clsFine.GetFineByID(GetFineID());
            if (fine != null)
            {
                frmAddNewFine addNewFine = new frmAddNewFine(fine);
                addNewFine.ShowDialog();
                GetFines(); 
                return;
            }

            clsUtilityLibrary.PrintWarningMessage("This fine record is not found");
        }
        private void SetFilter(string colName, string colValue)
        {
            DataTable fines = clsFine.GetAllFines();
            DataView dv = new DataView();
            dv = fines.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }

        private void txtFilter_TextChanged_1(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }
    }
}
