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
using System.IO;

namespace DVLD
{
    public partial class Form1 : Form
    {
        
        private void _ClearRemeber()
        {
            string Filename = @"D:\Important Project\DVLD Project\DVLD\Remember.txt";
            File.WriteAllText(Filename, string.Empty);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new PeopleManagementForm();
            frm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CurrentUserLabel.Text = clsUser.CurrentUser.UserID.ToString();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Users.UserManagement();
            frm.ShowDialog();
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ClearRemeber();
            this.Hide();
            this.Close();
            Form frm = new Login();
            frm.ShowDialog();
        }

        private void AccountInformationToolMenuStrip_Click(object sender, EventArgs e)
        {
            Form frm = new Users.UserInfoForm(clsUser.CurrentUser.UserID);
            frm.Show();
        }

        private void ChangePasswordToolMenuStrip_Click(object sender, EventArgs e)
        {
            Form frm = new Users.ChangePasswordForm(clsUser.CurrentUser.UserID,true);
            frm.ShowDialog();
            this.Close();
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.Types.ApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Tests.Types.TestTypesForm();
            frm.ShowDialog();  
        }

        private void localToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.SaveAddApplication(-1);
            frm.ShowDialog();
        }

        private void manageLocalApplToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.ManageLocalApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new People.ManageDrivers();
            frm.ShowDialog();
        }

        private void internationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.InternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void manageInternationalApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.ManageInternationalApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.RenewLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Applications.ReplacementForLostorrDamagedLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.DetainLicense();
            frm.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.ReleaseLicense(-1);
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new License.ManageDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
