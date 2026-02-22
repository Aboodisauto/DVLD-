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
using BussinessLayer;

namespace DVLD.License
{
    public partial class DetainLicense : Form
    {
        clsLicense _license;
        int DetainIDNumber;
        public DetainLicense()
        {
            InitializeComponent();
            UserName.Text = clsUser.CurrentUser.UserName;
            DetainDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }
        private void _DetainProcess() {
            if (_license == null)
            {
                MessageBox.Show("Please select a license to detain.");
                return;
            }
            DetainIDNumber = clsLicense.DetainLicense(_license.LicenseID, FineAmount.Value, clsUser.CurrentUser.UserID);
            if(DetainIDNumber == -1)
            {
                MessageBox.Show("Failed to detain the license. Please try again.");
                return;
            }
            DetainID.Text = DetainIDNumber.ToString();
            MessageBox.Show("License detained successfully.");
            linkLabel2.Enabled = true;
        }
        private void filterLicenses1_FoundLicense(clsLicense obj)
        {
            
            filterLicenses1._LoadData();
            LicenseID.Text = obj.LicenseID.ToString();
            _license = obj;
            linkLabel1.Enabled = true;
            button1.Enabled = true;
            if (clsLicense.isLicenseDetained(obj.LicenseID))
            {
                MessageBox.Show("This license is already detained.");
                button1.Enabled = false;
                return;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _DetainProcess();
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
    }
}
