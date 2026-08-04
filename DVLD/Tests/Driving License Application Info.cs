using BussinessLayer;
using DVLD.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class Driving_License_Application_Info : UserControl
    {
        public clsLocalApplication localApplication;

        public void _LoadDriverApplicationInfo(int LocalApplicationID)
        {
            localApplication = clsLocalApplication.Find(LocalApplicationID);
            AppIdLb.Text = localApplication.LocalApplicationID.ToString();
            LicenseClassNameLB.Text = clsLicenseClass.GetClassName(localApplication.LicenseClassID);
            int passedTests = clsTest.CountPassedTests(localApplication.LocalApplicationID);
            PassedTestLB.Text = $"{passedTests}/3";
            linkLabel1.Enabled = passedTests == 3 && localApplication.ApplicationStatus == 3;
        }

        public Driving_License_Application_Info()
        {
            InitializeComponent();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm showLicenseForm = new ShowLicenseForm(localApplication.ApplicationID);
            showLicenseForm.ShowDialog();
        }
    }
}
