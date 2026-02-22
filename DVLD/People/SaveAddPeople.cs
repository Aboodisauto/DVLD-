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

namespace DVLD.People
{
    public partial class SaveAddPeople : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public DataBackEventHandler dataBack;
        string newFileName;
        enum enMode { Add, Update};
        enMode mode = enMode.Add;

        int PersonID = -1;
        clsPerson person;
        bool DidEditImage = false;
        private void SetUpEnvironment()
        {
            dateTimePicker1.MaxDate = DateTime.Today.AddYears(-18);
            comboBox1.DataSource = clsCountries.FetchCountries();
            Malerb.Checked = true;
        }
        public SaveAddPeople(int PersonID)
        {
            InitializeComponent();
            SetUpEnvironment();
            this.PersonID = PersonID;
            if(PersonID == -1)
            {
                TextPurpose.Text = "Add User";
                mode = enMode.Add;
                person = new clsPerson();
                return;
            }
            mode = enMode.Update;
            person = clsPerson.Find(PersonID);
            if(person != null)
            {
                TextPurpose.Text = "Update User " + person.ID;
                _LoadPersonData();
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
                pictureBox1.Image = Image.FromFile(person.ImagePath);
                newFileName = person.ImagePath;
                removellb.Visible = true;
            }
            else
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
            person.ImagePath = newFileName;
            
            

        }
        private void Addllb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            openFileDialog1.Title = "Select Person Image";
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.InitialDirectory = "Photos";

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                removellb.Visible = true;
                DidEditImage = true;
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _SavePictureIntoDestinationFolder()
        {
            if (!DidEditImage)
                return;
            if (openFileDialog1.FileName == string.Empty)
                return;
            string destinationFolder = "C:\\DVLDPhotos";
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }
            
            if (mode == enMode.Add) 
            {
                string guid = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(openFileDialog1.FileName);
                newFileName = Path.Combine(destinationFolder, guid + extension);
            }
            
            File.Copy(openFileDialog1.FileName, newFileName, true);

        
        }
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to add this user ?","Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            _SavePictureIntoDestinationFolder();
            _LoadDataIntoPerson();
            
            if (person.Save())
            {
                
                MessageBox.Show("User information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(dataBack != null)
                    dataBack.Invoke(this, person.ID);
                this.Close();

            }
            else
            {
                MessageBox.Show("Failed to save user information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
