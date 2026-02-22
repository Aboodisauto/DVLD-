using BusinessLayer;
using BussinessLayer;
using DVLD_Bussiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class InternationalLicenseApplication : Form
    {
        clsLicense license;
        clsApplication application;
        clsInternationalLicense iLicense;
        public void _LoadApplicationBasicInfo()
        {
            AppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            IssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            ExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            Username.Text = clsUser.CurrentUser.UserName.ToString();
            Fees.Text = clsApplicationTypes.GetApplicationFees(6).ToString();
        }
        public InternationalLicenseApplication()
        {
            InitializeComponent();
            _LoadApplicationBasicInfo();
        }
        private bool CheckIfDriverHasAILicense() 
        {
            int ILID = clsDriver.GetDriverILID(license.DriverID);
            if(ILID > 0)
            {
                MessageBox.Show($"The driver already has an international license with the id {ILID}, you can't apply for another one");
                return true;
            }
            return false;
            
        }
        private void IssueILicense()
        {
            iLicense = new clsInternationalLicense();
            iLicense.ApplicationID = application.ApplicationID;
            iLicense.DriverID = license.DriverID;
            iLicense.IssuedUsingLocalLicenseID = license.LicenseID;
            iLicense.IssueDate = DateTime.Now;
            iLicense.ExpirationDate = DateTime.Now.AddYears(1);
            iLicense.IsActive = true;
            iLicense.CreatedByUserID = clsUser.CurrentUser.UserID;

        }
        private void _IssueProcess()
        {
            application = new clsApplication();
            application.ApplicantID = clsPerson.GetPersonIDByDriverID(license.DriverID);
            application.ApplicationDate = DateTime.Now;
            application.ApplicationType = 6;
            application.ApplicationStatus = 1;
            application.StatusDate = DateTime.Now;
            application.PaidFees = double.Parse(Fees.Text);
            application.CreatedByUserID = clsUser.CurrentUser.UserID;
            if (application.Save())
            {
                IssueILicense();
                if(iLicense.Save())
                {
                    MessageBox.Show("The international license has been issued successfully");
                }
                else
                {
                    MessageBox.Show("An error occurred while issuing the international license");
                    return;
                }
            }
            else
            {
                MessageBox.Show("An error occurred while saving the application");
                return;
            }
            AppID.Text = application.ApplicationID.ToString();
            InternationalLicenseID.Text = iLicense.InternationalLicenseID.ToString();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filterLicenses1_FoundLicense(clsLicense obj)
        {
            license = obj;
            if (CheckIfDriverHasAILicense())
                return;
            if (license.LicenseClass != 3)
            {
                MessageBox.Show("The driver does not have a valid local license of class 3");
                return;
            }
            filterLicenses1._LoadData();
            LocalLicenseID.Text = license.LicenseID.ToString();
            linkLabel1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _IssueProcess();
            linkLabel2.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LocalApplicationID = clsLocalApplication.GetLocalApplicationIDByLicenseID(license.LicenseID);
            Form frm = new License.LicenseHistory(LocalApplicationID);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new License.InternationalLicenseForm(iLicense.InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
