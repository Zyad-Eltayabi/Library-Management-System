using Database_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Tier
{
    public class clsAdmin
    {
        public int AdminID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public enum Mode { Add = 1, Update = 2 }
        public Mode enMode { get; set; }

        public clsAdmin(string fullName, string userName, string password, bool isActive)
        {
            FullName = fullName;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            enMode = Mode.Add;
        }

        private clsAdmin(int adminID, string fullName, string userName, string password, bool isActive, Mode enMode)
        {
            AdminID = adminID;
            FullName = fullName;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            this.enMode = enMode;
        }

        public static clsAdmin GetAdminByID(int adminID)
        {
            string fullName = string.Empty, userName = string.Empty, password = string.Empty;
            bool isActive = false;

            if (clsAdminDB.GetAdminByID(adminID, ref fullName, ref userName, ref password, ref isActive))
                return new clsAdmin(adminID, fullName, userName, password, isActive, Mode.Update);

            return null;
        }

        private bool AddNewAdmin()
        {
            string encryptedPassword = clsHashing.ComputeHash(Password);
            this.AdminID = clsAdminDB.AddNewAdmin(UserName, encryptedPassword, IsActive, FullName);
            return AdminID != -1;
        }

        public static DataTable GetAllAdmins()
        {
            return clsAdminDB.GetAllAdmins();
        }

        public static bool DeleteAdmin(int adminID)
        {
            return clsAdminDB.DeleteAdmin(adminID);
        }

        private bool UpdateAdmin()
        {
            string encryptedPassword = clsHashing.ComputeHash(Password);
            return clsAdminDB.UpdateAdmin(AdminID, FullName, UserName, encryptedPassword, IsActive);
        }

        public static DataTable GetAdminByUserNameAndPassword(string userName, string password)
        {
            string encryptedPassword = clsHashing.ComputeHash(password);

            return clsAdminDB.GetAdminByUserNameAndPassword(userName, encryptedPassword);
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

        public static DataTable GetAdminsCount()
        {
            return clsAdminDB.GetAdminsCount();
        }
    }
}
