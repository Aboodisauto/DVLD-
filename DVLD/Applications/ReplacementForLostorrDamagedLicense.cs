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
    public partial class ReplacementForLostorrDamagedLicense : Form
    {
        clsApplication _application;
        clsLicense _license;
        enum enApplicationType { Lost = 3, Damaged = 4}
        enApplicationType ReplacementType;
        private void _LoadApplication()
        {
            AppFees.Text = clsApplicationTypes.GetApplicationFees((int)ReplacementType).ToString();
            UserName.Text = clsUser.CurrentUser.UserName;
            AppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }
        public ReplacementForLostorrDamagedLicense()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            ReplacementType = enApplicationType.Lost;
            _LoadApplication();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                ReplacementType = enApplicationType.Lost;
                _LoadApplication();
            }
            else
            {
                ReplacementType = enApplicationType.Damaged;
                _LoadApplication();
            }
        }

        private void filterLicenses1_FoundLicense(clsLicense obj)
        {
            filterLicenses1._LoadData();
            OldLicenseID.Text = obj.LicenseID.ToString();
            linkLabel1.Enabled = true;
            button1.Enabled = true;
            _license = obj;
            if(!obj.IsActive)
            {
                MessageBox.Show("This license is not active. Please select an active license.", "Invalid License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Enabled = false;
                return;
            }    
        }
        private bool _CreateApplication() 
        {
            _application = new clsApplication();
            _application.ApplicantID = clsPerson.GetPersonIDByDriverID(_license.DriverID);
            _application.ApplicationStatus = 1;
            _application.ApplicationType = (int)ReplacementType;
            _application.ApplicationDate = DateTime.Now;
            _application.CreatedByUserID = clsUser.CurrentUser.UserID;
            _application.PaidFees = double.Parse(AppFees.Text);
            _application.StatusDate = DateTime.Now;
            if (_application.Save())
            {
                return true;
            }
            return false;
        }
        private void _SaveProcess()
        {
            if(!_CreateApplication())
            {
                MessageBox.Show("An error has occurred while creating the application. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _license.IsActive = false;
            if (_license.Save())
            {
                clsLicense NewLicense = new clsLicense();
                NewLicense.DriverID = _license.DriverID;
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.Notes = string.Empty;
                NewLicense.IssueReason = (int)ReplacementType;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClass.GetLicensePeriodInYears(_license.LicenseClass));
                NewLicense.LicenseClass = _license.LicenseClass;
                NewLicense.ApplicationID = _application.ApplicationID;
                NewLicense.CreatedByUserID = clsUser.CurrentUser.UserID;
                NewLicense.IsActive = true;
                if (NewLicense.Save())
                {
                    _license = NewLicense;
                    MessageBox.Show($"A Replacement Has Been issued with id = {_license.LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            MessageBox.Show("An Error Has Occured While Issuing The License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            _SaveProcess();
        }
    }
}
