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
        public AddSaveUser()
        {
            InitializeComponent();
        }
        public AddSaveUser(int UserId)
        {
            InitializeComponent();
            this.UserId = UserId;
            m = mode.Edit;
        }
        private void _LoadUserData()
        {
           
            if(user != null)
            {
                idLabel.Text = user.UserID.ToString();
                Usernametb.Text = user.UserName;
                Passwordtb.Text = string.Empty;
                cPasswordtb.Text = string.Empty;
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
            user.Password = Util.clsUtil.HashPassword(Passwordtb.Text.ToString());
            user.isActive = checkBox1.Checked;
        }
        private bool _LoadDataIntoUser()
        {
            user = clsUser.GetUserByID(PersonID);
            if (user == null)
            {
                user.PersonID = PersonID;
                LoadData();
                return true;
            }
            else
            {
                if (m != mode.Edit)
                {
                    MessageBox.Show("User Already Exists for this person !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                LoadData();
            }
            return true;
           
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
            Savebtn.Enabled = true;
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
            if (!_LoadDataIntoUser())
            {
                return;
            }
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
        private void ResetDefaultValues()
        {
            Usernametb.Text = "";
            Passwordtb.Text = "";
            cPasswordtb.Text = "";
            checkBox1.Checked = false;
            filterPeople1.PersonID = -1;
            if(m == mode.Add)
            {
                filterPeople1.Enabled = true;
                Savebtn.Enabled = false;
                button1.Enabled = false;
                user = new clsUser();
                this.Text = "Add New User";
            }
            else
            {
                filterPeople1.Enabled = false;
                button1.Enabled = true;
                Savebtn.Enabled = true;
                user = clsUser.Find(UserId);
                PersonID = user.PersonID;
                this.Text = "Edit User";
            }
        }
        private void AddSaveUser_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();
            if(m == mode.Edit)
            {
                _LoadUserData();
            }
        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
