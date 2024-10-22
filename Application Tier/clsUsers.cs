using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsUsers
    {
        public int UserID { get; set; }
        public string LibraryCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime MembershipDate { get; set; }
        public int NationalityID { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        private clsUsers(int userID, string libraryCardNumber, string firstName, string lastName, DateTime dateOfBirth, bool gender,
            string email, string phoneNumber, string address, DateTime membershipDate, int nationalityID, Mode enMode)
        {
            UserID = userID;
            LibraryCardNumber = libraryCardNumber;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            MembershipDate = membershipDate;
            NationalityID = nationalityID;
            this.enMode = enMode;
        }

        public clsUsers(string libraryCardNumber, string firstName, string lastName, DateTime dateOfBirth, bool gender,
            string email, string phoneNumber, string address, DateTime membershipDate, int nationalityID, Mode enMode)
        {
            LibraryCardNumber = libraryCardNumber;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            MembershipDate = membershipDate;
            NationalityID = nationalityID;
            this.enMode = enMode;
        }

        private bool AddNewUser()
        {
            this.UserID = clsUsersDB.AddNewUser(LibraryCardNumber, FirstName, LastName, DateOfBirth, Gender,
             Email, PhoneNumber, Address, MembershipDate, NationalityID);

            return UserID != -1;
        }

        private bool UpdateAuthor()
        {
            return clsUsersDB.UpdateUser(UserID, LibraryCardNumber, FirstName, LastName, DateOfBirth, Gender,
             Email, PhoneNumber, Address, MembershipDate, NationalityID);
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    {
                        enMode = Mode.Update;
                        return AddNewUser();
                    }
                case Mode.Update:
                    {
                        return UpdateAuthor();
                    }
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDB.GetAllUsers();
        }

        public static clsUsers GetUserByID(int userID)
        {
            string libraryCardNumber = "", firstName = "", lastName = "", email = "", phoneNumber = "", address = "";
            DateTime dateOfBirth = DateTime.Now;
            bool gender = true;
            DateTime membershipDate = DateTime.Now; int nationalityID = -1;

            if (clsUsersDB.GetUserByID(userID, ref libraryCardNumber, ref firstName, ref lastName, ref dateOfBirth, ref gender,
            ref email, ref phoneNumber, ref address, ref membershipDate, ref nationalityID))
                return new clsUsers(userID, libraryCardNumber, firstName, lastName, dateOfBirth, gender,
             email, phoneNumber, address, membershipDate, nationalityID, clsUsers.Mode.Update);

            return null;
        }

        public static bool DoesUserExist(int userID)
        {
            return clsUsersDB.DoesUserExist(userID);
        }
    }
}
