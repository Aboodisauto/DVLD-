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

namespace DVLD.License
{
    public partial class InternationalLicenseForm : Form
    {
        public InternationalLicenseForm(int internationalLicenseID)
        {
            InitializeComponent();
            internationalLicenseInfo1.ILicense = clsInternationalLicense.Find(internationalLicenseID);
            internationalLicenseInfo1._LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
