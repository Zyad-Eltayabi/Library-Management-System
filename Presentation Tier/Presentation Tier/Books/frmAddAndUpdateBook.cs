using Guna.UI2.WinForms;
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
    public partial class frmAddAndUpdateBook : Form
    {
        public frmAddAndUpdateBook()
        {
            InitializeComponent();
        }

        private bool ValidateTextBoxes()
        {
            Guna2TextBox[] textBoxes = { txtBookTitle, txtISBN, txtGenre };
            return clsUtilityLibrary.ValidateTextBoxes(textBoxes);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if(!ValidateTextBoxes())
                return;
        }
    }
}
