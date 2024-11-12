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
            GetALlAuthors();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddOrUpdateAuthor addOrUpdateAuthor = new frmAddOrUpdateAuthor();
            addOrUpdateAuthor.ShowDialog();
            GetALlAuthors();
        }

        private void frmManageAuthors_Load(object sender, EventArgs e)
        {
            GetALlAuthors();
        }

        private void GetALlAuthors()
        {
            dgvTable.DataSource = clsAuthor.GetAllAuthors();
            lbRecords.Text = dgvTable.RowCount.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int authorID = GetAuthorID();

            if (MessageBox.Show($"Are you sure to delete this Author where ID = {authorID}", "Info", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (clsAuthor.DeleteAuthor(authorID))
                {
                    clsUtilityLibrary.PrintInfoMessage("Deleted successfully.");
                    GetALlAuthors();
                }
                else
                {
                    clsUtilityLibrary.PrintErrorMessage("Failed to delete.");
                }
            }
        }

        private int GetAuthorID()
        {
            return int.Parse(dgvTable.SelectedRows[0].Cells["AuthorID"].Value.ToString());
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddOrUpdateAuthor addOrUpdateAuthor = new frmAddOrUpdateAuthor(GetAuthorID());
            addOrUpdateAuthor.ShowDialog();
            GetALlAuthors();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            SetFilter(cbFilter.Text.ToString(), txtFilter.Text.ToString());
        }

        private void SetFilter(string colName, string colValue)
        {
            DataTable _authorsTable = clsAuthor.GetAllAuthors();
            DataView dv = new DataView();
            dv = _authorsTable.DefaultView;
            if (!string.IsNullOrWhiteSpace(colValue))
                dv.RowFilter = string.Format(@"CONVERT([{0}], System.String) LIKE '{1}%'", colName, colValue);
            dgvTable.DataSource = dv;
        }

    }
}
