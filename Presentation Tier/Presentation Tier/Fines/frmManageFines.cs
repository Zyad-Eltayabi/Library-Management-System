using Application_Tier;
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
            dgvTable.DataSource = clsFines.GetAllFines();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private int GetFineID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["FineID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            clsFines fine = clsFines.GetFineByID(GetFineID());
            if (fine != null)
            {
                frmAddNewFine addNewFine = new frmAddNewFine(fine);
                addNewFine.ShowDialog();
                return;
            }

            clsUtilityLibrary.PrintWarningMessage("This fine record is not found");
        }
    }
}
