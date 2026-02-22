using BussinessLayer;
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
    public partial class UserManagement : Form
    {
        DataTable Users = clsUser.FetchUsers();
        private string[] GetDataTableColumns(DataTable table)
        {
            string[] columns = new string[table.Columns.Count + 1];
            columns[0] = "None";
            foreach (DataColumn col in table.Columns)
            {
                columns[col.Ordinal + 1] = col.ColumnName;
            }
            return columns;
        }
        private void _RefreshUsers(DataTable users)
        {
            dataGridView1.DataSource = users;
        }
        public UserManagement()
        {
            InitializeComponent();
            comboBox1.DataSource = GetDataTableColumns(Users);
            _RefreshUsers(Users);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selected = comboBox1.SelectedValue.ToString();
            switch (Selected)
            {
                case "Person ID":
                    maskedTextBox1.Visible = true;
                    textBox1.Visible = false;
                    break;
                case "User ID":
                    textBox1.Visible = false;
                    maskedTextBox1.Visible = true;
                    break;
                case "None":
                    maskedTextBox1.Visible = false;
                    textBox1.Visible = false;
                    break;
                default:
                    textBox1.Visible = true;
                    maskedTextBox1.Visible = false;
                    break;

            }
        }
        private void FilterNumericData(string keyWord,string value)
        {
            Users.DefaultView.RowFilter = $"{keyWord} = {value}";
        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedText == "Person ID")
            {
                FilterNumericData("PersonID", maskedTextBox1.Text.ToString());
            }
            else
            {
                FilterNumericData("UserID", maskedTextBox1.Text.ToString());
            }
            _RefreshUsers(Users);
        }
        private void FilterData(string Selected,string SelectedValue)
        {
            if (SelectedValue == string.Empty)
                Users.DefaultView.RowFilter = string.Empty;
            Users.DefaultView.RowFilter = $"{Selected} LIKE '{SelectedValue}%'";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string Selected = comboBox1.SelectedValue.ToString();
            string Value = textBox1.Text.ToString();
            FilterData(Selected, Value);
            _RefreshUsers(Users);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new UserInfoForm(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new AddSaveUser(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            Users = clsUser.FetchUsers();
            _RefreshUsers(Users);
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            if (clsUser.DeleteUser(UserId))
            {
                MessageBox.Show("User Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Users = clsUser.FetchUsers();
                _RefreshUsers(Users);
            }
            else
            {
                MessageBox.Show("Failed to Delete User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new AddSaveUser(-1);
            frm.ShowDialog();
            Users = clsUser.FetchUsers();
            _RefreshUsers(Users);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ChangePasswordForm((int)dataGridView1.SelectedRows[0].Cells[0].Value, false);
            frm.ShowDialog();
            Users = clsUser.FetchUsers();
            _RefreshUsers(Users);
        }
    }
}
