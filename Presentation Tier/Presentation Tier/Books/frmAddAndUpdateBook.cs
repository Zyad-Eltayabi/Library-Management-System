using Application_Tier;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Tier.Books
{
    public partial class frmAddAndUpdateBook : Form
    {
        DataTable _authorsDetails;
        private string _imagePath;
        private string _imageName;

        private enum Mode { Add = 1, Update = 2 }
        private Mode _enMode { get; set; }

        public frmAddAndUpdateBook()
        {
            InitializeComponent();
            _enMode = Mode.Add;
        }

        private bool ValidateTextBoxes()
        {
            Guna2TextBox[] textBoxes = { txtBookTitle, txtISBN, txtGenre };
            return clsUtilityLibrary.ValidateTextBoxes(textBoxes);
        }


        private string GetBooksImagesFolderPath()
        {
            string booksImagesPath = $@"{Application.StartupPath}\Book_Images";
            if (!Directory.Exists(booksImagesPath))
                Directory.CreateDirectory($@"{Application.StartupPath}\Book_Images");

            return booksImagesPath;
        }

        private void CopyImage(string sourceImagePath)
        {
            string newImageName = Guid.NewGuid().ToString();
            File.Copy(sourceImagePath, $@"{GetBooksImagesFolderPath()}\{newImageName}.jpg");
            _imagePath = $@"{GetBooksImagesFolderPath()}\{newImageName}.jpg";
            _imageName = $"{newImageName}.jpg";
        }

        private void ReadImage()
        {
            if (!string.IsNullOrEmpty(_imagePath) && File.Exists(_imagePath))
            {
                using (FileStream imageFile = new FileStream(_imagePath, FileMode.Open, FileAccess.Read))
                {
                    picBook.Image = System.Drawing.Image.FromStream(imageFile);
                }
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Choose Image";
            ofd.Filter = "JPG (*.jpg)|*.jpg|PNG (*.PNG)|*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    CopyImage(ofd.FileName);
                    ReadImage();

                }
                catch (Exception ex)
                {
                    clsUtilityLibrary.PrintErrorMessage(ex.Message);
                }
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            picBook.Image = null;
            if (!string.IsNullOrEmpty(_imagePath) && File.Exists(_imagePath))
            {
                File.Delete(_imagePath);
            }
        }

        private void frmAddAndUpdateBook_Load(object sender, EventArgs e)
        {
            GetAuthorsNames();
        }

        private void GetAuthorsNames()
        {
            _authorsDetails = clsAuthors.GetAuthorsNames();
            cbAuthor.DataSource = _authorsDetails;
            cbAuthor.ValueMember = "FullName";
        }

        private int GetAuthorID()
        {
            return int.Parse(_authorsDetails.Rows[cbAuthor.SelectedIndex]["AuthorID"].ToString());
        }

        private void AddNewBook()
        {
            clsBooks newBook = new clsBooks(
                txtBookTitle.Text.ToString(),
                txtISBN.Text.ToString(),
                dtPublicationDate.Value,
                txtGenre.Text.ToString(),
                txtAdditionalDetails.Text.ToString(),
                _imageName,
                 GetAuthorID()
                );

            if (newBook.Save())
            {
                lbBookID.Text = newBook.BookID.ToString();
                clsUtilityLibrary.PrintInfoMessage("Book Added Successfully");
            }
            else
            {
                clsUtilityLibrary.PrintErrorMessage("Failed Operation");
            }
        }

        private void SaveBook()
        {
            switch (_enMode)
            {
                case Mode.Add:
                    _enMode = Mode.Update;
                    AddNewBook();
                    break;
                case Mode.Update:
                    break;
                default:
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!ValidateTextBoxes())
                return;
            SaveBook();
        }
    }
}
