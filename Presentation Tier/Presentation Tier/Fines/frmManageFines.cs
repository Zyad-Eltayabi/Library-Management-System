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
    }
}
