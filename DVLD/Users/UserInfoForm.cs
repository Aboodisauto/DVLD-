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
    public partial class UserInfoForm : Form
    {
        public UserInfoForm(int UserId)
        {
            InitializeComponent();
            clsUser user = clsUser.Find(UserId);
            userInfo1.user = user;
            userInfo1.LoadData();
            userInfo1.peopleInformation1.linkLabel1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
