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

namespace Presentation_Tier.Reservations
{
    public partial class frmManageReservations : Form
    {
        public frmManageReservations()
        {
            InitializeComponent();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int reservationID = GetReservationID();
            clsReservation reservation = clsReservation.GetReservationRecordByID(reservationID);
            if (reservation != null)
            {
                frmCreateNewReservation updateReservation = new frmCreateNewReservation(reservation);
                updateReservation.ShowDialog();
                GetReservations();
                return;
            }

            clsUtilityLibrary.PrintWarningMessage("There was not a reservation with this ID Number");
        }

        private int GetReservationID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["ReservationID"].Value.ToString());
        }

        private void frmManageReservations_Load(object sender, EventArgs e)
        {
            GetReservations();
        }

        private void GetReservations()
        {
            dgvTable.DataSource = clsReservation.GetAllReservations();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }

        private void SetFilter(string colName, string colValue)
        {
            DataTable reservations =clsReservation.GetAllReservations();
            DataView dv = new DataView();
            dv = reservations.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }
    }
}
