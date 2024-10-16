using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
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

        private clsAdmins(int adminID, string fullName, string userName, string password, bool isActive, Mode enMode)
        {
            AdminID = adminID;
            FullName = fullName;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            this.enMode = enMode;
        }

        public static clsAdmins GetAdminByID(int adminID)
        {
            string fullName = string.Empty, userName = string.Empty, password = string.Empty;
            bool isActive = false;

            if (clsAdminsDB.GetAdminByID(adminID, ref fullName, ref userName, ref password, ref isActive))
                return new clsAdmins(adminID, fullName, userName, password, isActive, Mode.Update);

            return null;
        }

        private bool AddNewAdmin()
        {
            string encryptedPassword = clsHashing.ComputeHash(Password);
            this.AdminID = clsAdminsDB.AddNewAdmin(UserName, encryptedPassword, IsActive, FullName);
            return AdminID != -1;
        }

        public static DataTable GetAllAdmins()
        {
            return clsAdminsDB.GetAllAdmins();
        }

        public static bool DeleteAdmin(int adminID)
        {
            return clsAdminsDB.DeleteAdmin(adminID);
        }

        private bool UpdateAdmin()
        {
            string encryptedPassword = clsHashing.ComputeHash(Password);
            return clsAdminsDB.UpdateAdmin(AdminID, FullName, UserName, encryptedPassword, IsActive);
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
                        return UpdateAdmin();
                    }
            }
            return false;
        }
    }
}
