using BussinessLayer;
using DVLD.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{

    public partial class Login : Form
    {
        string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD\RememberMe";
        string valueName = "RememberMe";
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
            string keyValue = Username + ":" + Password;
            try
            {
                Registry.SetValue(keyPath, valueName, keyValue, RegistryValueKind.String);
            }catch(Exception ex)
            {
                Console.WriteLine("Error While Logging Data: \n" + ex.Message);
            }
        }
        private void _ShowMainForm()
        {
            this.Hide();
            Form frm = new Form1(this);
            frm.ShowDialog();
            
        }
        private bool _ReadDataOnOpen()
        {
            //if (!File.Exists(FilePath))
            //    return false;
            //string[] lines = File.ReadAllLines(FilePath);
            //if (lines.Length <= 0)
            //    return false;
            //if (!clsUser.Login(lines[0], lines[1]))
            //{
            //    return false;
            //}
            //return true;
            string UserPass = Registry.GetValue(keyPath, valueName, null) as string;
            if(UserPass != null)
            {
                string[] lines = UserPass.Split(':');
                if (lines[0] == "" || !clsUser.Login(lines[0], lines[1]))
                    return false;
            }
            else
            {
                Registry.SetValue(keyPath, valueName, "");
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
                clsUtil.RemeberMe(userName, password);
            }

            _ShowMainForm();
            
        }
    }
}
