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

namespace DVLD.People
{
    public partial class PeopleInformation : UserControl
    {
        public int PersonId { set; get; }
       
        clsPerson person;
        public void _LoadData() 
        {
            person = clsPerson.Find(PersonId);

            IDlb.Text = PersonId.ToString();
            nameLB.Text = person.FullName;
            NationalNoLB.Text = person.NationalNo;
            PhoneLB.Text = person.MobileNo;
            CountryLB.Text = clsCountries.CountryNamee(person.CountryID);
            AddressLB.Text = person.Address;
            EmailLB.Text = person.Email;
            DateOfBithLB.Text = person.BirthDate.ToShortDateString();
            if (person.Gender == 0)
            {
                pictureBox1.Image = Resources.Male_512;
                GenderLB.Text = "Male";
            }
            else
            {
                pictureBox1.Image = Resources.Female_512;
                GenderLB.Text = "Female";
            }
            pictureBox1.ImageLocation = person.ImagePath;

        }
        public PeopleInformation()
        {
            InitializeComponent();
        }
        
        private void PeopleInformation_Load(object sender, EventArgs e)
        {
            
        }
    }
}
