using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier
{
    public static class clsUtilityLibrary
    {
        public static void PrintInfoMessage(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        public static void PrintErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }

        public static void PrintWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static void CenterForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;
        }

        public static bool ValidateTextBoxes(Guna2TextBox[] textBoxes)
        {
            foreach (Guna2TextBox box in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    clsUtilityLibrary.PrintWarningMessage("Some fields are required");
                    return false;
                }
            }
            return true;
        }
    }
}
