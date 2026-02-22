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
using System.IO;

namespace DVLD
{
    public partial class Login : Form
    {
        string FilePath = @"D:\Important Project\DVLD Project\DVLD\Remember.txt";
        public Login()
        {
            InitializeComponent();
            if (_ReadDataOnOpen())
            {
                _ShowMainForm();
                
            }
            
        }
        private void _RemeberData(string Username,string Password)
        {
            string[] info = { Username, Password };
            
            File.WriteAllLines(FilePath, info);
        }
        private void _ShowMainForm()
        {
            Form frm = new Form1();
            this.Close();
            frm.ShowDialog();
            
        }
        private bool _ReadDataOnOpen()
        {
            if (!File.Exists(FilePath))
                return false;
            string[] lines = File.ReadAllLines(FilePath);
            if (lines.Length <= 0)
                return false;
            if (!clsUser.Login(lines[0], lines[1]))
            {
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = UsernameTB.Text.ToString();
            string password = PasswordTB.Text.ToString();
            if (!clsUser.Login(userName, password))
            {
                MessageBox.Show("Invalid Username/Password !","Invalid",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked)
            {
                _RemeberData(userName,password);
            }
            
            _ShowMainForm();
            
        }
    }
}
