using BussinessLayer;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (person == null)
            {
                MessageBox.Show("Person Not Found !");
                return;
            }
            IDlb.Text = PersonId.ToString();
            nameLB.Text = person.FullName;
            NationalNoLB.Text = person.NationalNo;
            PhoneLB.Text = person.MobileNo;
            CountryLB.Text = clsCountries.CountryNamee(person.CountryID);
            AddressLB.Text = person.Address;
            EmailLB.Text = person.Email;
            DateOfBithLB.Text = person.BirthDate.ToShortDateString();
            _LoadPersonImage();

        }
        private void _LoadPersonImage()
        {
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
            string ImagePath = person.ImagePath;
            if(ImagePath != null && ImagePath != "")
            {
                if(File.Exists(ImagePath))
                {
                    pictureBox1.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Image Not Found !");
                }
            }
        }
        public PeopleInformation()
        {
            InitializeComponent();
        }
        
        private void PeopleInformation_Load(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveAddPeople saveAddPeople = new SaveAddPeople(PersonId);
            saveAddPeople.ShowDialog();
            _LoadData();
        }
    }
}
