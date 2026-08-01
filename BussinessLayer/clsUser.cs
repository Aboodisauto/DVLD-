using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BussinessLayer
{
    public class clsUser
    {
        public static clsUser CurrentUser;
        enum enMode { AddNew, Update };
        enMode mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }

        private clsUser(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;
            this.PersonInfo = clsPerson.Find(PersonID);
            mode = enMode.Update;
        }
        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            isActive = false;
        }
        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "",Password= "";
            bool isActive = false;
            if(clsUserDAC.FindUserByID(UserID, ref PersonID, ref UserName, ref Password, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        private bool _AddUser() {
        return clsUserDAC.AddUser(PersonID, UserName, Password, isActive);
        }
        private bool _UpdateUser()
        {
            return clsUserDAC.UpdateUser(UserID, PersonID, UserName, Password, isActive);
        }
        public bool Save() 
        {
            bool isSaved = false;
            switch (mode)
            {
                case enMode.AddNew:
                    isSaved = _AddUser();
                    break;
                case enMode.Update:
                    isSaved = _UpdateUser();
                    break;
            }
            return isSaved;
        }
        public static bool DeleteUser(int UserID)
        {
            return clsUserDAC.DeleteUser(UserID);
        }
        public static DataTable FetchUsers()
        {
            return clsUserDAC.FetchUsers();
        }
        public static bool Login(string username, string password)
        {
            int UserID = clsUserDAC.GetUserID(username, clsSecuirty.ComputeHash(password));
            if (UserID != -1)
            {
                CurrentUser = clsUser.Find(UserID);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static clsUser GetUserByID(int PersonID) 
        {
            int UserId = -1;
            string Username = "", Password = "";
            bool isActive = false;
            if(clsUserDAC.GetUserByPersonID(PersonID, ref UserId, ref Username, ref Password, ref isActive))
            {
                return new clsUser(UserId, PersonID, Username, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static string GetUserName(int UserID)
        {
            return clsUserDAC.GetUserName(UserID);
        }
        public static bool HashAllPasswords()
        {
            int[] Ids = clsUser.FetchUsers().AsEnumerable().Select(r => r.Field<int>("User ID")).ToArray();
            foreach (int id in Ids)
            {
                clsUser user = clsUser.Find(id);
                if (user != null)
                {
                    user.Password = clsSecuirty.ComputeHash(user.Password);
                    user.Save();
                }
            }
            return true;
        }
    }
}
