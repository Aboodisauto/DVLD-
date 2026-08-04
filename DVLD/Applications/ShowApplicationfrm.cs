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
    public partial class ShowApplicationfrm : Form
    {
        int LocalApplicationID;
        public ShowApplicationfrm(int LocalApplicationID)
        {
            InitializeComponent();
            this.LocalApplicationID = LocalApplicationID;
        }

        private void ShowApplicationfrm_Load(object sender, EventArgs e)
        {
            
            applicationBasicInfo1._LoadLocalApplicationData(LocalApplicationID);
            driving_License_Application_Info1._LoadDriverApplicationInfo(LocalApplicationID);
        }
    }
}
