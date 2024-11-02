namespace Presentation_Tier.Books
{
    partial class frmShowBookDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.ucShowBookDetails1 = new Presentation_Tier.Books.ucShowBookDetails();
            this.SuspendLayout();
            // 
            // ucShowBookDetails1
            // 
            this.ucShowBookDetails1.Location = new System.Drawing.Point(2, 3);
            this.ucShowBookDetails1.Name = "ucShowBookDetails1";
            this.ucShowBookDetails1.Size = new System.Drawing.Size(924, 691);
            this.ucShowBookDetails1.TabIndex = 0;
            this.ucShowBookDetails1.Load += new System.EventHandler(this.ucShowBookDetails1_Load);
            // 
            // frmShowBookDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 711);
            this.Controls.Add(this.ucShowBookDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowBookDetails";
            this.Text = " Book Details";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private ucShowBookDetails ucShowBookDetails1;
    }
}