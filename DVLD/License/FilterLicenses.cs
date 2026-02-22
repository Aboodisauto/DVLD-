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

namespace DVLD.License
{
    public partial class FilterLicenses : UserControl
    {
        public clsLicense license;
        public event Action<clsLicense> FoundLicense;
        public virtual void OnFoundLicense(clsLicense license)
        {
            Action<clsLicense> handler = FoundLicense;
            if (handler != null)
            {
                handler(license);
            }
        }
        public FilterLicenses()
        {
            InitializeComponent();
        }
        public void _LoadData()
        {
            textBox1.Text = license.LicenseID.ToString();
            driver_License_Info1.localApplication = clsApplication.Find(license.LicenseID);
            if(driver_License_Info1.localApplication == null)
            {
                driver_License_Info1.license = license;
            }
            driver_License_Info1._LoadLocalApplicationData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            license = clsLicense.Find(Convert.ToInt32(textBox1.Text));
            if(license == null)
            {
                MessageBox.Show("No license found with the provided ID.");
                return;
            }
            if (FoundLicense != null)
                OnFoundLicense(license);
        }
    }
}
