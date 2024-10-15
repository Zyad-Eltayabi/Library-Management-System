using Database_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsAdmins
    {
        public int AdminID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsAdmins(string fullName, string userName, string password, bool isActive)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            enMode = Mode.Add;
        }

        private bool AddNewAdmin()
        {
            string encryptedPassword = clsHashing.ComputeHash(Password);
            this.AdminID = clsAdminsDB.AddNewAdmin(UserName, encryptedPassword, IsActive, FullName);
            return AdminID != -1;
        }

        public bool Save()
        {
            switch (enMode)
            {
                case Mode.Add:
                    {
                        enMode = Mode.Update;
                        return AddNewAdmin();
                    }
                    case Mode.Update:
                    {
                        break;
                    }
            }
            return false;
        }
    }
}
