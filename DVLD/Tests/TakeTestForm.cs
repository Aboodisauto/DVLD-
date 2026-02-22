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

namespace DVLD.Tests
{
    public partial class TakeTestForm : Form
    {
        clsTestAppointment TestAppointment;
        clsLocalApplication LocalApplication;
        clsTest Test;
        private void _ChangeTheFormAccordingToTestType(int TestType)
        {
            switch (TestType)
            {
                case 1:
                    groupBox1.Text = "Vision";
                    pictureBox1.Image = Resources.Vision_512;
                    break;
                case 2:
                    groupBox1.Text = "Written";
                    pictureBox1.Image = Resources.Written_Test_512;
                    break;
                case 3:
                    groupBox1.Text = "Street";
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
            }
        }
        private void _LoadAppointmentData()
        {
            IDLB.Text = TestAppointment.LocalApplicationID.ToString();
            ClassLB.Text = clsLicenseClass.GetClassName(LocalApplication.LicenseClassID);
            NameLB.Text = clsApplication.GetApplicantFullName(LocalApplication.ApplicantID);
            CountLB.Text = clsTest.CalCulateTestTrail(LocalApplication.LocalApplicationID, TestAppointment.TestTypeID).ToString();
            DateLB.Text = TestAppointment.AppointmentDate.ToString("dd/MMM/yyyy");
            MoneyLB.Text = TestAppointment.TestPaidFees.ToString();
        }
        public TakeTestForm(int TestAppointmentID)
        {
            InitializeComponent();
            TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            LocalApplication = clsLocalApplication.Find(TestAppointment.LocalApplicationID);
            _ChangeTheFormAccordingToTestType(TestAppointment.TestTypeID);
            _LoadAppointmentData();
            Test = clsTest.Find(TestAppointmentID);
            if (Test != null)
                TestIDLB.Text = Test.testID.ToString();
            else
                Test = new clsTest();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadDataIntoTest()
        {
            Test.testAppointmentID = TestAppointment.TestAppointmentID;
            Test.testResult = (PassRB.Checked == true) ? true : false;
            Test.UserID = clsUser.CurrentUser.UserID;
            Test.Notes = NotesTB.Text;
        }
        private void _SaveProcess()
        {
            _LoadDataIntoTest();
            if (!Test.Save())
            {
                MessageBox.Show("There Was An Error During The Saving Process,\n Check Logs.txt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            clsApplication App = clsApplication.Find(TestAppointment.RetakeApplicationID);
            if(App != null)
                App.ApplicationStatus = 3;
            MessageBox.Show("Process Was a Success !", "Success", MessageBoxButtons.OK);
            TestIDLB.Text = Test.testID.ToString();
            TestAppointment.isLocked = true;
            TestAppointment.Save();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _SaveProcess();
        }
    }
}
