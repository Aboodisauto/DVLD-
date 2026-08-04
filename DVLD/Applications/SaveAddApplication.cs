using BusinessLayer;
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
    public partial class SaveAddApplication : Form
    {
        enum Mode { Add,Edit};
        Mode m = Mode.Add;
        int PersonID = -1;
        clsLocalApplication LocalApplication;
        private void _fillLicensesClasses()
        {
            comboBox1.DataSource = clsLicenseClass.LicenseClasses().ToArray();
        }
        private int _GetTheIndexForSelectedClass(string ClassName)
        {
            for(int i = 0; i  < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i].ToString().Contains(ClassName))
                {
                    return i;
                }
            }
            return -1;
        }
        private void _LoadDataIntoPerson()
        {
            if (m != Mode.Edit)
                LocalApplication.ApplicationDate = DateTime.Now;
            LocalApplication.ApplicantID = PersonID;
            LocalApplication.ApplicationStatus = 1;
            LocalApplication.StatusDate = DateTime.Now;
            LocalApplication.ApplicationType = 1;
            LocalApplication.CreatedByUserID = clsUser.CurrentUser.UserID;
            LocalApplication.LicenseClassID = clsLocalApplication.GetLicenseClassID(comboBox1.SelectedItem.ToString());
            LocalApplication.PaidFees = Convert.ToDouble(FeesLB.Text);
        }
        private bool _CheckForLicense()
        {
            int LicenseClassID = clsLicenseClass.GetClassID(comboBox1.SelectedItem.ToString());
            return clsLicense.DoesPersonAlreadyHasALicense(PersonID,LicenseClassID);
        }
        private void _LoadPersonsData()
        {
            UsernameLB.Text = LocalApplication.PersonFullName;
            comboBox1.SelectedIndex = LocalApplication.LicenseClassID-1;
            FeesLB.Text = clsLicenseClass.getClassFees(comboBox1.SelectedItem.ToString()).ToString();
            IDLB.Text = LocalApplication.ApplicationID.ToString();
            DateLB.Text = LocalApplication.ApplicationDate.ToShortDateString();
            filterPeople1.PersonID = LocalApplication.ApplicantID;
            filterPeople1.LoadPersonData();
        }
        private void _ChangeClassFeesLabel()
        {
            FeesLB.Text = clsLicenseClass.getClassFees(comboBox1.SelectedItem.ToString()).ToString();
        }
        public SaveAddApplication(int LocalApplicationID)
        {
            InitializeComponent();
            UsernameLB.Text = clsUser.CurrentUser.UserName;
            DateLB.Text = DateTime.Now.ToShortDateString();
            _fillLicensesClasses();
            _ChangeClassFeesLabel();
            if(LocalApplicationID == -1)
            {
                LocalApplication = new clsLocalApplication();
                return;
            }

            filterPeople1.Enabled = false;
            LocalApplication = clsLocalApplication.Find(LocalApplicationID);
            _LoadPersonsData();
            m = Mode.Edit;
        }

        private void filterPeople1_FoundPerson(int obj)
        {
            if(obj != -1)
                PersonID = obj;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ChangeClassFeesLabel();
        }
    
        private bool CheckDuplicatesOfTheSameClass()
        {
            return clsApplication.IsThereaDuplicate(PersonID, clsLicenseClass.GetClassID(comboBox1.SelectedItem.ToString()));
        }
        private void SaveProcess()
        {
            this.PersonID = filterPeople1.PersonID;
            if(PersonID < 0)
            {
                MessageBox.Show("Select A Person First to continue !");
                return;
            }
            if (_CheckForLicense())
            {
                MessageBox.Show("This Person Does Have A License For This Class !", "Stop !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            if(CheckDuplicatesOfTheSameClass())
            {
                MessageBox.Show("An Existing Application With That LicenseClass is ongoing !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadDataIntoPerson();
            if (LocalApplication.Save())
            {
                MessageBox.Show("Application Saved Successfuly !", "Success", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            MessageBox.Show("There was an error saving Application !");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveProcess();
        }
    }
}
