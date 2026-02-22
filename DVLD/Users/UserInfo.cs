using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
namespace DVLD.Users
{
    public partial class UserInfo : UserControl
    {
        public clsUser user;
        public UserInfo()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            peopleInformation1.PersonId = user.PersonID;
            peopleInformation1._LoadData();
            UserIDlb.Text = user.UserID.ToString();
            Usernamelb.Text = user.UserName.ToString();
            if (user.isActive)
                ActiveLB.Text = "Yes";
            else
                ActiveLB.Text = "No";
        }

        private void UserInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
