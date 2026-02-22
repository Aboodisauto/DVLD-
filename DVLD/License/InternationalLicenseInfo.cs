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
using BussinessLayer;
using BusinessLayer;
using DVLD.Properties;

namespace DVLD.License
{
    public partial class InternationalLicenseInfo : UserControl
    {
        public clsInternationalLicense ILicense;
        clsPerson person;
        public void _LoadData()
        {
            person = clsPerson.Find(clsPerson.GetPersonIDByDriverID(ILicense.DriverID));
            NameLB.Text = person.FullName;
            DateOfBirthLB.Text = person.BirthDate.ToString("dd/MMM/yyyy");
            if(person.Gender == 0)
            {
                GendorLB.Text = "Male";
                GendorPB.Image = Resources.Man_32;
            }
            else
            {
                GendorLB.Text = "Female";
                GendorPB.Image = Resources.Woman_32;
            }
            intIDLB.Text = ILicense.InternationalLicenseID.ToString();
            AppID.Text = ILicense.ApplicationID.ToString();
            IDLB.Text = ILicense.IssuedUsingLocalLicenseID.ToString();
            IssueDateLB.Text = ILicense.IssueDate.ToString("dd/MMM/yyyy");
            ExpirationDateLB.Text = ILicense.ExpirationDate.ToString("dd/MMM/yyyy");
            IsActiveLB.Text = ILicense.IsActive ? "Yes" : "No";
            DriverIDLB.Text = ILicense.DriverID.ToString();
            if(person.ImagePath != null)
            {
                try
                {
                    PersonIMG.Image = Image.FromFile(person.ImagePath);
                }
                catch
                {
                    PersonIMG.Image = Resources.Male_512;
                }
            }
            else
            {
                PersonIMG.Image = Resources.Male_512;
            }
        }
        public InternationalLicenseInfo()
        {
            InitializeComponent();
        }
    }
}
