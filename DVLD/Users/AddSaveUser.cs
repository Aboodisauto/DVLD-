using BussinessLayer;
using DVLD.People;
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
    public partial class AddSaveUser : Form
    {

        int UserId = -1, PersonID = -1;
        enum mode { Add, Edit }
        mode m = mode.Add;
        clsUser user;
        public AddSaveUser(int UserId)
        {
            InitializeComponent();
            if(UserId != -1)
            {
                this.UserId = UserId;
                user = clsUser.Find(UserId);
                filterPeople1.Enabled = false;
                
                m = mode.Edit;
                _LoadUserData();
                return;
            }
            user = new clsUser();
        }
        private void _LoadUserData()
        {
           
            if(user != null)
            {
                idLabel.Text = user.UserID.ToString();
                Usernametb.Text = user.UserName;
                Passwordtb.Text = user.Password;
                cPasswordtb.Text = user.Password;
                checkBox1.Checked = user.isActive;
                filterPeople1.PersonID = user.PersonID;
                filterPeople1.LoadPersonData();
            }
            else
            {
                MessageBox.Show("No User Found for this person !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void LoadData()
        {
            user.UserName = Usernametb.Text.ToString();
            user.Password = Passwordtb.Text.ToString();
            user.isActive = checkBox1.Checked;
        }
        private void _LoadDataIntoUser()
        {
            user = clsUser.GetUserByID(PersonID);
            if (user == null)
            {
                user = new clsUser();
                user.PersonID = PersonID;
                LoadData();
            }
            else
            {
                if (m != mode.Edit)
                {
                    MessageBox.Show("User Already Exists for this person !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadData();
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (filterPeople1.PersonID == -1)
            {
                MessageBox.Show("Please Select a valid person !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            user.PersonID = filterPeople1.PersonID;
            tabControl1.SelectedIndex = 1;
        }

        private void filterPeople1_FoundPerson(int obj)
        {
            PersonID = obj;
            button1.Enabled = true;
        }
        private bool CheckPassword()
        {
            return cPasswordtb.Text == Passwordtb.Text;
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (!CheckPassword())
            {
                cPasswordtb.Focus();
                errorProvider1.SetError(cPasswordtb, "The passwords doesn't match");
                return;
            }
            _LoadDataIntoUser();
            if(user.Save())
            {
                MessageBox.Show("User Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error Saving User !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new SaveAddPeople(filterPeople1.PersonID);
            frm.ShowDialog();
            filterPeople1.LoadPersonData();
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
