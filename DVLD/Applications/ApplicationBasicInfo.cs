using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ApplicationBasicInfo : UserControl
    {
        public clsLocalApplication localApplication;
        public void _LoadApplicationData(int ID)
        {
            localApplication = clsLocalApplication.Find(ID);
        }
        private string DetermineStatus()
        {
            short passedTests = clsTest.CountPassedTests(localApplication.LocalApplicationID);
            if (localApplication.ApplicationStatus == 1 && passedTests == 0)
            {
                return "New";
            }
            else if (localApplication.ApplicationStatus == 1 && passedTests > 0)
                return "OnGoing";
            else if (localApplication.ApplicationStatus == 2)
                return "Cancelled";
            else if (localApplication.ApplicationStatus == 3)
                return "Completed";

            return "Unknown";
        }
        public void _LoadLocalApplicationData()
        {
            AppIdLb.Text = localApplication.LocalApplicationID.ToString();
            StatusLB.Text = DetermineStatus();
            FeesLB.Text = localApplication.PaidFees.ToString();
            TypeLB.Text = clsApplicationTypes.GetApplicationTitle(localApplication.ApplicationType);
            ApplicantName.Text = clsApplication.GetApplicantFullName(localApplication.ApplicantID);
            DateLB.Text = localApplication.ApplicationDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            StatusDateLB.Text = localApplication.StatusDate.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            UsernameLB.Text = clsUser.CurrentUser.UserName;
        }
        public ApplicationBasicInfo()
        {
            InitializeComponent();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new People.ShowPeopleForm(localApplication.ApplicantID);
            frm.ShowDialog();
        }
    }
}
