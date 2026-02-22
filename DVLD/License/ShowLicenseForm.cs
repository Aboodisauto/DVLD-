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
    public partial class ShowLicenseForm : Form
    {
        public ShowLicenseForm(int ApplicationID)
        {
            InitializeComponent();
            clsApplication application = clsApplication.Find(ApplicationID);
            driver_License_Info1.localApplication = application;
            driver_License_Info1._LoadLocalApplicationData();
        }
    }
}
