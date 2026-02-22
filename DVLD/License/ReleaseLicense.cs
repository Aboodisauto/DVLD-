using BusinessLayer;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.License
{
    public partial class ReleaseLicense : Form
    {
        clsLicense _license;
        clsApplication _application;
        clsLicense.clsDetain Detain;
        private void _LoadBasicInfo()
        {
            UserName.Text = clsUser.CurrentUser.UserName;
            AppFees.Text = clsApplicationTypes.GetApplicationFees(5).ToString();
        }
        private void _CalculateTotalFees()
        {

            decimal appFees = 0;
            decimal fineAmount = 0;

            // Safely attempt to parse the text. If it fails, the variables remain 0.
            decimal.TryParse(AppFees.Text, out appFees);
            decimal.TryParse(FineAmount.Text, out fineAmount);

            decimal TotalFees = appFees + fineAmount;
            TotalFeesAmount.Text = TotalFees.ToString();

        }
        public ReleaseLicense(int LicenseID)
        {
            InitializeComponent();
            _LoadBasicInfo();
            if(LicenseID == -1){
                return;
            }
            _license = clsLicense.Find(LicenseID);
            if (_license == null)
            {
                MessageBox.Show("License not found.");
                this.Close();
                return;
            }
            filterLicenses1.license = _license;
            filterLicenses1._LoadData();
            filterLicenses1.Enabled = false;
            filterLicenses1_FoundLicense(_license);
        }

        private void filterLicenses1_FoundLicense(clsLicense obj)
        {
            _license = obj;
            LicenseID.Text = _license.LicenseID.ToString();
            filterLicenses1._LoadData();
            if (!clsLicense.isLicenseDetained(_license.LicenseID))
            {
                MessageBox.Show("This license is not detained.");
                button1.Enabled = false;
                return;
            }
            button1.Enabled = true;
            Detain = clsLicense.GetDetainInfoByLicenseID(_license.LicenseID);
            DetainID.Text = Detain.DetainID.ToString();
            DetainDate.Text = Detain.DetainDate.ToString("dd/MMM/yyyy");
            FineAmount.Text = Detain.FineFees.ToString();
            _CalculateTotalFees();
            linkLabel1.Enabled = true;
            linkLabel2.Enabled = false;
        }
        private bool _CreateAnApplication() 
        {
            _application = new clsApplication();
            _application.ApplicantID = clsPerson.GetPersonIDByDriverID(_license.DriverID);
            _application.ApplicationStatus = 1;
            _application.ApplicationType = 5;
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
        private void _ReleaseLicense()
        {
            if (!_CreateAnApplication())
            {
                MessageBox.Show("Failed to create an application. License release failed.");
                return;
            }
            if (clsLicense.ReleaseLicense(_license.LicenseID,clsUser.CurrentUser.UserID,_application.ApplicationID))
            {
                AppID.Text = _application.ApplicationID.ToString();
                _application.ApplicationStatus = 3;
                if (_application.Save())
                {
                    linkLabel2.Enabled = true;
                    MessageBox.Show("License released successfully.");
                }
                else
                {
                    MessageBox.Show("License released but failed to update application status.");
                }
            }
            else
            {
                MessageBox.Show("Failed to release license.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _ReleaseLicense();
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
