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
using BusinessLayer;

namespace DVLD.License
{
    public partial class IssueLicenseForm : Form
    {
        clsLicense License;
        clsDriver Driver;
        clsLocalApplication localApplication;
        public IssueLicenseForm(int LocalApplicationID)
        {
            InitializeComponent();
            localApplication = clsLocalApplication.Find(LocalApplicationID);
            applicationBasicInfo1._LoadLocalApplicationData(LocalApplicationID);
            driving_License_Application_Info1._LoadDriverApplicationInfo(LocalApplicationID);
        }
        private void _AddDriver()
        {
            if (clsDriver.IsDriverExistByPersonID(localApplication.ApplicantID))
            {
                Driver = clsDriver.FindByPersonID(localApplication.ApplicantID);
                return;
            }
            Driver = new clsDriver();
            Driver.PersonID = localApplication.ApplicantID;
            Driver.CreatedByUserID = clsUser.CurrentUser.UserID;
            Driver.CreatedDate = DateTime.Now;
            Driver.Save();
                
        }
        private void _LoadDataIntoLicense()
        {
            License = new clsLicense();
            License.ApplicationID = localApplication.ApplicationID;
            License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicensePeriodInYears(localApplication.LicenseClassID)) ;
            License.IssueDate = DateTime.Now;
            License.CreatedByUserID = clsUser.CurrentUser.UserID;
            License.DriverID = Driver.DriverID;
            License.IsActive = true;
            License.IssueReason = 1;
            License.LicenseClass = localApplication.LicenseClassID;
            License.PaidFees = Convert.ToDecimal(localApplication.PaidFees);
            License.Notes = textBox1.Text.ToString();
        }
        private void _IssueLicense()
        {
            _AddDriver();
            _LoadDataIntoLicense();
            if (License.Save())
            {
                localApplication.ApplicationStatus = 3;
                localApplication.StatusDate = DateTime.Now;
                if(localApplication.Save())
                    MessageBox.Show($"Operation Successfull, The LicenseID = {License.LicenseID}", "Success");
                return;
            }
            MessageBox.Show("There was an error check the logs !", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _IssueLicense();
        }
    }
}
