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
using System.IO;
using System.Net.Mail;
using DVLD.Util;

namespace DVLD.People
{
    public partial class SaveAddPeople : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public DataBackEventHandler dataBack;
        enum enMode { Add, Update};
        enMode mode = enMode.Add;

        int PersonID = -1;
        clsPerson person;
        private void SetUpEnvironment()
        {
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Today.AddYears(-100);

            comboBox1.DataSource = clsCountries.FetchCountries();
            Malerb.Checked = true;
            DetermineByMode();
        }
        private void DetermineByMode()
        {
            if (mode == enMode.Add)
            {
                TextPurpose.Text = "Add User";
                person = new clsPerson();
                return;
            }
            person = clsPerson.Find(this.PersonID);
            if (person != null)
            {
                TextPurpose.Text = "Update User " + person.ID;
                _LoadPersonData();
            }
        }
        public SaveAddPeople()
        {
            InitializeComponent();
            mode = enMode.Add;
        }
        public SaveAddPeople(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
            mode = enMode.Update;
            
        }
        private void SetImageBasedOnGender()
        {
            if (person.Gender == 0)
            {
                Malerb.Checked = true;
                pictureBox1.Image = Resources.Male_512;
            }
            else
            {

                Femalerb.Checked = true;
                pictureBox1.Image = Resources.Female_512;
            }
        }
        private void _LoadPersonData()
        {
            firstTb.Text = person.FirstName;
            secondtb.Text = person.SecondName;
            thirdTB.Text = person.ThirdName;
            LastTB.Text = person.LastName;
            NationalNotb.Text = person.NationalNo;
            dateTimePicker1.Value = person.BirthDate;
            Addresstb.Text = person.Address; 
            phonetb.Text = person.MobileNo;
            EmailTB.Text = person.Email;
            comboBox1.SelectedIndex = person.CountryID - 1;
            if(person.ImagePath != string.Empty && File.Exists(person.ImagePath))
            {
                pictureBox1.ImageLocation = person.ImagePath;
                removellb.Visible = true;
            }
            else
            {
                SetImageBasedOnGender();
            }
        }
        
        public bool isValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void EnablingErrorProvider(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                textBox.Focus();
                errorProvider1.SetError(textBox, "incorrect or no value ");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox, "");
            }
        }

        private void firstTb_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void secondtb_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void thirdTB_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void LastTB_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void NationalNotb_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
            if(NationalNotb.Text.Trim() != person.NationalNo && clsPerson.DoesPersonExist(NationalNotb.Text.Trim()))
            {
                e.Cancel = true;
                NationalNotb.Focus();
                errorProvider1.SetError(NationalNotb, "This National Number already exists.");
            }
        }

        private void phonetb_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void EmailTB_Validating(object sender, CancelEventArgs e)
        {
            if(isValidEmail(EmailTB.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(EmailTB, "");
            }
            else
            {
                e.Cancel = true;
                EmailTB.Focus();
                errorProvider1.SetError(EmailTB, "Invalid Email Format");
            }
        }

        private void Addresstb_Validating(object sender, CancelEventArgs e)
        {
            EnablingErrorProvider(sender, e);
        }

        private void Malerb_CheckedChanged(object sender, EventArgs e)
        {
            if(Malerb.Checked)
            {
                pictureBox1.Image = Resources.Male_512;
            }
            else
            {
                pictureBox1.Image = Resources.Female_512;

            }
        }
        private void _LoadDataIntoPerson()
        {
            person.FirstName = firstTb.Text;
            person.SecondName = secondtb.Text;
            person.ThirdName = thirdTB.Text;
            person.LastName = LastTB.Text;
            person.NationalNo = NationalNotb.Text;
            person.BirthDate = dateTimePicker1.Value;
            if (Malerb.Checked)
            {
                person.Gender = 0;
            }
            else
            {
                person.Gender = 1;
            }
            person.Address = Addresstb.Text;
            person.MobileNo = phonetb.Text;
            person.Email = EmailTB.Text;
            person.CountryID = comboBox1.SelectedIndex + 1;
            person.ImagePath = pictureBox1.ImageLocation;
            
            

        }
        private void Addllb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            openFileDialog1.Title = "Select Person Image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.InitialDirectory = "Photos";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.Image = null;
                removellb.Visible = true;
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool _HandleImage()
        {
            if(person.ImagePath != pictureBox1.ImageLocation)
            {
                if(person.ImagePath != string.Empty && File.Exists(person.ImagePath))
                {
                    try
                    {
                        File.Delete(person.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to delete the old image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            if(pictureBox1.ImageLocation != null)
            {
                string sourceFilePath = pictureBox1.ImageLocation;
                if(clsUtil.CopyFileToImageFolder(ref sourceFilePath))
                {
                    pictureBox1.ImageLocation = sourceFilePath;
                    return true;
                }
                return false;
            }
            return true;
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to add this user ?","Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            
            if (!_HandleImage())
            {
                return;
            }
            _LoadDataIntoPerson();
            if (person.Save())
            {
                
                MessageBox.Show("User information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(dataBack != null)
                    dataBack.Invoke(this, person.ID);
                mode = enMode.Update;
                this.Close();

            }
            else
            {
                MessageBox.Show("Failed to save user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAddPeople_Load(object sender, EventArgs e)
        {
            SetUpEnvironment();
        }

        private void removellb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.ImageLocation = null;
            removellb.Visible = false;
            if (Malerb.Checked)
            {
                pictureBox1.Image = Resources.Male_512;
            }
            else
            {
                pictureBox1.Image = Resources.Female_512;
            }
        }
    }
}
