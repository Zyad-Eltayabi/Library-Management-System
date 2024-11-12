using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsAuthor
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public bool Gender { get; set; }
        public int NationalityID { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsAuthor(string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath, bool gender, int nationalityID)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
            Gender = gender;
            NationalityID = nationalityID;
            enMode = Mode.Add;
        }

        private clsAuthor(int authorID, string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath, bool gender, int nationalityID, Mode enMode)
        {
            AuthorID = authorID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
            Gender = gender;
            NationalityID = nationalityID;
            this.enMode = enMode;
        }

        public static clsAuthor GetAuthorByID(int authorID)
        {
            string firstName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            DateTime? dateOfDeath = null;
            bool gender = true;
            int nationalityID = -1;

            if (clsAuthorDB.GetAuthorByID(authorID, ref firstName, ref lastName, ref dateOfBirth, ref dateOfDeath, ref gender, ref nationalityID))
                return new clsAuthor(authorID, firstName, lastName, dateOfBirth, dateOfDeath, gender, nationalityID, Mode.Update);

            return null;

        }

        private bool AddNewAuthor()
        {
            this.AuthorID = clsAuthorDB.AddNewAuthor(FirstName, LastName, DateOfBirth, DateOfDeath, Gender, NationalityID);
            return AuthorID != -1;
        }

        private bool UpdateAuthor()
        {
            return clsAuthorDB.UpdateAuthor(AuthorID, FirstName, LastName, DateOfBirth, DateOfDeath, Gender, NationalityID);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    {
                        enMode = Mode.Update;
                        return AddNewAuthor();
                    }
                case Mode.Update:
                    {
                        return UpdateAuthor();
                    }
            }
            return false;
        }

        public static DataTable GetAllAuthors()
        {
            return clsAuthorDB.GetAllAuthors();
        }

        public static bool DeleteAuthor(int authorID)
        {
            return clsAuthorDB.DeleteAuthor(authorID);
        }

        public static DataTable GetAuthorsNames()
        {
            return clsAuthorDB.GetAuthorsNames();
        }

        public string AuthorFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
