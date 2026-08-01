using BussinessLayer;
using DVLD.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD.Users
{
    public partial class ChangePasswordForm : Form
    {
        int UserID = -1;
        clsUser User;
        bool ChangeCurrentPassword;
        public ChangePasswordForm(int UserID, bool State)
        {
            InitializeComponent();
            this.UserID = UserID;
            User = clsUser.Find(UserID);
            ChangeCurrentPassword = State;                                                               
            IDlb.Text = UserID.ToString();
        }
        private bool CheckCurrentUser()
        {
            if ((User.UserID == clsUser.CurrentUser.UserID) && !ChangeCurrentPassword)
                return false;
            return true ;
        }
        private bool _ChangePassword()
        {
            bool Changed = false;
            User.Password = clsUtil.HashPassword(NewPasswordTb.Text.Trim());
            if (User.Save())
            {
                Changed = true;
            }
            else
            {
                User.Password = Currenttb.Text;
            }
                return Changed;
        }
        private bool CheckPasswordValidation()
        {
            return (NewPasswordTb.Text == Confirmationtb.Text); 
            
        }
        private bool CheckPasswordLength()
        {
            return (NewPasswordTb.Text.Length > 3);
        }
        private bool CheckCurrentPassword()
        {
            return (clsUtil.HashPassword(Currenttb.Text) == User.Password);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckCurrentPassword())
            {
                errorProvider1.SetError(Currenttb, "This Password is not Valid !");
                Currenttb.Focus();
                return;
            }
            if (!CheckPasswordLength())
            {
                errorProvider1.SetError(NewPasswordTb, "This Password is not long enough !");
                NewPasswordTb.Focus();
                return;
            }
            if (!CheckPasswordValidation())
            {
                errorProvider1.SetError(NewPasswordTb, "The Password Doesn't Match !");
                errorProvider1.SetError(Confirmationtb, "The Password Doesn't Match !");
                NewPasswordTb.Focus();
                return;
            }
            if (!CheckCurrentUser())
            {
                MessageBox.Show("Cannot Change the Password For Current User");
                this.Close();
            }
            if (!_ChangePassword())
            {
                MessageBox.Show("The Password Has Not Been Changed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("The Password Has Been Changed !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();

        }
    }
}
