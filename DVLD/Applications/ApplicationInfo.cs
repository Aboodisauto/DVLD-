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
    public partial class ApplicationInfo : UserControl
    {
        public void _LoadApplicationBasicInfo()
        {
            AppDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            IssueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            ExpirationDate.Text = DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy");
            Username.Text = clsUser.CurrentUser.UserName.ToString();
            Fees.Text = clsApplicationTypes.GetApplicationFees(6).ToString();
        }
        public ApplicationInfo()
        {
            InitializeComponent();
        }
    }
}
