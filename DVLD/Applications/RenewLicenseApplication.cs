using BusinessLayer;
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

namespace DVLD.Applications
{
    public partial class RenewLicenseApplication : Form
    {
        clsLicense _license;
        clsApplication application;
        public void _LoadApplicationBasicInfo()
        {
            AppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            IssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            Username.Text = clsUser.CurrentUser.UserName.ToString();
            Fees.Text = clsApplicationTypes.GetApplicationFees(2).ToString();
        }
        private bool _CheckIfLicenseIsExpired()
        {
            DateTime Expiration = _license.ExpirationDate;
            return DateTime.Now > Expiration;
        }
        private bool _CheckIfLicenseIsActive()
        {
            return _license.IsActive;
        }
        public RenewLicenseApplication()
        {
            InitializeComponent();
            _LoadApplicationBasicInfo();
            Fees.Text = clsApplicationTypes.GetApplicationFees(2).ToString();
        }
        private bool IssueApplication()
        {
                application = new clsApplication();
                application.ApplicationType = 2;
                application.ApplicationDate = DateTime.Now;
                application.StatusDate = DateTime.Now;
            application.ApplicationStatus = 1;
            application.ApplicantID = clsPerson.GetPersonIDByDriverID(_license.DriverID);
                application.ApplicationDate = DateTime.Now;
                application.PaidFees = Convert.ToDouble(TotalFees.Text);
                application.CreatedByUserID = clsUser.CurrentUser.UserID;
                if (application.Save())
                {
                    return true;
                }
                else
                {
                return false;
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IssueApplication())
            {
                _license.IsActive = false;
                if(!_license.Save())
                {
                    MessageBox.Show("An error occurred while renewing the license.");
                    return;
                }
                clsLicense newLicense = new clsLicense();
                newLicense.ApplicationID = application.ApplicationID;
                newLicense.DriverID = _license.DriverID;
                newLicense.LicenseClass = _license.LicenseClass;
                newLicense.IssueDate = DateTime.Now;
                newLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicensePeriodInYears(_license.LicenseClass));
                newLicense.Notes = textBox1.Text;
                newLicense.IssueReason = (int)clsLicense.enIssueReason.Renew;
                newLicense.PaidFees = Convert.ToDecimal(TotalFees.Text);
                newLicense.CreatedByUserID = clsUser.CurrentUser.UserID;
                newLicense.IsActive = true;
                if (newLicense.Save())
                {
                    _license = newLicense;
                    AppID.Text = application.ApplicationID.ToString();
                    newLicneseID.Text = newLicense.LicenseID.ToString();
                    ExpirationDate.Text = newLicense.ExpirationDate.ToString("dd/MMM/yyyy");
                    textBox1.Enabled = false;
                    application.ApplicationStatus = 3;
                    application.StatusDate = DateTime.Now;
                    if (!application.Save())
                    {
                        return;
                    }
                    MessageBox.Show($"License Renewed Successfully, With Id = {newLicense.LicenseID}");
                }

            }
            else
            {
                MessageBox.Show("An error occurred while renewing the license.");
            }
        }
        private void _CalculateFees() 
        {
            if(_license != null)
            {
                LicenseFees.Text = clsLicenseClass.getClassFees(_license.LicenseClass).ToString();
            }
            TotalFees.Text = (Convert.ToDouble(Fees.Text) + Convert.ToDouble(LicenseFees.Text)).ToString();
        }
        private void filterLicenses1_FoundLicense(BusinessLayer.clsLicense obj)
        {
            if (obj == null)
                return;
            _license = obj;
            filterLicenses1.license = _license;
            filterLicenses1._LoadData();
            OldLicenseID.Text = _license.LicenseID.ToString();
            linkLabel1.Enabled = true;
            _CalculateFees();
            if (!_CheckIfLicenseIsExpired())
            {
                MessageBox.Show("The license is not expired yet, you cannot renew it.");
                button1.Enabled = false;
                return;
            }
            if(!_CheckIfLicenseIsActive())
            {
                MessageBox.Show("The license has been renewed, you cannot renew it.");
                button1.Enabled = false;
                return;
            }
            button1.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new License.LicenseHistory(_license.ApplicationID);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new License.ShowLicenseForm(_license.ApplicationID);
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            linkLabel2.Enabled = true;
        }
    }
}
