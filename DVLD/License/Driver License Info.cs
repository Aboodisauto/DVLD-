using BusinessLayer;
using BussinessLayer;
using DVLD.Properties;
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
    public partial class Driver_License_Info : UserControl
    {
        public clsApplication localApplication;
        private clsPerson person;
        public clsLicense license;
        public void _LoadLocalApplicationData()
        {
            if(localApplication == null)
            {
                localApplication = clsApplication.Find(license.ApplicationID);
            }
            else
            {
                license = clsLicense.FindByApplicationID(localApplication.ApplicationID);
            }
            ClassLB.Text = clsLicenseClass.GetClassName(license.LicenseClass);
            NameLB.Text = clsApplication.GetApplicantFullName(localApplication.ApplicantID);
            NationalNoLB.Text = clsPerson.GetPersonNationalNo(localApplication.ApplicantID);
            person = clsPerson.Find(localApplication.ApplicantID);
            if (license != null)
                {
                    IDLB.Text = license.LicenseID.ToString();
                    IssueDateLB.Text = license.IssueDate.ToShortDateString();
                    ExpirationDateLB.Text = license.ExpirationDate.ToShortDateString();
                if (license.Notes != "" || license.Notes != string.Empty)
                    NotesLB.Text = license.Notes;
                else
                    NotesLB.Text = "No Notes";
                    IssueReasonLB.Text = clsLicense.GetIssueReasonText(license.IssueReason);
                }
            DateOfBirthLB.Text = person.BirthDate.ToString("dd/MMM/yyyy");
            if (person.Gender == 0)
            {
                GendorLB.Text = "Male";
                GendorPB.Image = Resources.Man_32;
            }
            else
            {
                GendorLB.Text = "Female";
                GendorPB.Image = Resources.Woman_32;
            }
            IsActiveLB.Text = license.IsActive ? "Active" : "Inactive";
            ExpirationDateLB.Text = license.ExpirationDate.ToString("dd/MMM/yyyy");
            DriverIDLB.Text = clsDriver.GetDriverIDByPersonID(localApplication.ApplicantID).ToString();
            IsDetainedLB.Text = clsLicense.isLicenseDetained(license.LicenseID) ? "Yes" : "No";
            if(person.ImagePath != null && person.ImagePath != "")
            {
                try
                {
                    PersonIMG.Image = Image.FromFile(person.ImagePath);
                }
                catch
                {
                    PersonIMG.Image = Resources.Person_32;
                }
            }
            else
            {
                PersonIMG.Image = Resources.Person_32;
            }
            
        }
        public Driver_License_Info()
        {
            InitializeComponent();
        }
    }
}
