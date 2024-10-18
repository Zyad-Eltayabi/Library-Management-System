using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsAuthors
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

        public clsAuthors(string firstName, string lastName, DateTime dateOfBirth, DateTime? dateOfDeath, bool gender, int nationalityID)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
            Gender = gender;
            NationalityID = nationalityID;
            enMode = Mode.Add;
        }

        private bool AddNewAuthor()
        {
            this.AuthorID = clsAuthorsDB.AddNewAuthor(FirstName, LastName, DateOfBirth,DateOfDeath,Gender,NationalityID);
            return AuthorID != -1;
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
                       // return UpdateAdmin();
                       break;
                    }
            }
            return false;
        }

        public static DataTable GetAllAuthors()
        {
            return clsAuthorsDB.GetAllAuthors();
        }



    }
}
