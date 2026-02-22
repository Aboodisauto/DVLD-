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

namespace DVLD.Tests
{
    public partial class Driving_License_Application_Info : UserControl
    {
        public clsLocalApplication localApplication;
        public void _LoadApplicationInformation(int ID)
        {
            localApplication = clsLocalApplication.Find(ID);
        }
        public void _RefreshLocalApplication()
        {
            AppIdLb.Text = localApplication.LocalApplicationID.ToString();
            LicenseClassNameLB.Text = clsLicenseClass.GetClassName(localApplication.LicenseClassID);
            PassedTestLB.Text = $"{clsTest.CountPassedTests(localApplication.LocalApplicationID)}/3";
        }

        public Driving_License_Application_Info()
        {
            InitializeComponent();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
