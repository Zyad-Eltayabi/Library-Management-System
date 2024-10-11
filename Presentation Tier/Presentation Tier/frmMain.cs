﻿using Presentation_Tier.Admins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewFormToMainPanel(Form form)
        {
            pnMainPanel.Controls.Clear();

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;  // Remove borders to make it look integrated

            pnMainPanel.Controls.Add(form);
            form.Show();

        }

        private void btnAdmins_Click(object sender, EventArgs e)
        {
            AddNewFormToMainPanel(new frmAdmins());
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnAdmins.PerformClick();
        }
    }
}
