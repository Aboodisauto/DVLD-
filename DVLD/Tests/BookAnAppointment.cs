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
    public partial class BookAnAppointment : Form
    {
        enum enBookMethod { New , Retake }
        enum enMode { New , Update }
        enMode Mode = enMode.New;
        enBookMethod Method = enBookMethod.New;
        clsLocalApplication LApplication;
        clsTestAppointment appointment;
        int TestTypeID;
        private enBookMethod _CheckForRetake()
        {
            int NumberOfTrails = Convert.ToInt32(CountLB.Text);
            if(NumberOfTrails > 0)
            {
                return enBookMethod.Retake;
            }
            return enBookMethod.New;
        }
        private void _ChangeTheFormAccordingToTestType(int TestType)
        {
            switch (TestType)
            {
                case 1:

                    groupBox1.Text = "Vision Test";
                    pictureBox1.Image = Resources.Vision_512;
                    break;
                case 2:

                    groupBox1.Text = "Written Test";
                    pictureBox1.Image = Resources.Written_Test_512;
                    break;
                case 3:
                    groupBox1.Text = "Street Test";
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
            }
        }
        private void _LoadData()
        {
            _ChangeTheFormAccordingToTestType(TestTypeID);
            IDLB.Text = LApplication.LocalApplicationID.ToString();
            ClassLB.Text = clsLicenseClass.GetClassName(LApplication.LicenseClassID);
            NameLB.Text = LApplication.ApplicantInfo.FullName;
            CountLB.Text = clsTest.CalCulateTestTrail(LApplication.LocalApplicationID, TestTypeID).ToString();
            MoneyLB.Text = clsTestsTypes.GetTestTypeFees(TestTypeID).ToString();
            Method = _CheckForRetake();
            if(Method == enBookMethod.Retake)
            {
                groupBox2.Enabled = true;
                label3.Text = clsTestsTypes.GetRetakeTestFees().ToString();

            }
            TotalFeesLB.Text = (Convert.ToInt32(label3.Text) + Convert.ToInt32(MoneyLB.Text)).ToString();
            if(Mode == enMode.Update)
            {
                dateTimePicker1.Value = appointment.AppointmentDate;
            }
        }
        public BookAnAppointment(int TestAppointmentID,clsLocalApplication LApplication, int TestTypeID)
        {
            InitializeComponent();
            this.LApplication = LApplication;
            this.TestTypeID = TestTypeID;
            if(TestAppointmentID > 0)
            {
                appointment = clsTestAppointment.Find(TestAppointmentID);
                Mode = enMode.Update;
            }
            else
            {
                appointment = new clsTestAppointment();
                Mode = enMode.New;
            }
            _LoadData();
        }
        private void _LoadDataIntoTestAppointment()
        {
            appointment.TestCreatedUser = clsUser.CurrentUser.UserID;
            appointment.LocalApplicationID = LApplication.LocalApplicationID;
            appointment.TestTypeID = TestTypeID;
            appointment.TestPaidFees = clsTestsTypes.GetTestTypeFees(TestTypeID);
            appointment.AppointmentDate = dateTimePicker1.Value;
            appointment.isLocked = false;
        }
        private int _AddRetakeApplication()
        {
            clsApplication application = new clsApplication();
            application.ApplicationStatus = 1;
            application.ApplicantID = LApplication.ApplicantID;
            application.ApplicationDate = DateTime.Now;
            application.StatusDate = DateTime.Now;
            application.ApplicationType = 7;
            application.CreatedByUserID = clsUser.CurrentUser.UserID;
            if (application.Save())
            {
                return application.ApplicationID;
            }
            return -1;
        }
        private void _SaveProcess()
        {
            _LoadDataIntoTestAppointment();
            if (Method == enBookMethod.Retake)
            {
                appointment.RetakeApplicationID = _AddRetakeApplication();
                label7.Text = appointment.RetakeApplicationID.ToString();
            }
            if (appointment.Save())
            {
                MessageBox.Show("Appointment Booked SuccessFuly !", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            MessageBox.Show("An Error Has Happened When Booking The Appointment !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _SaveProcess();
        }
    }
}
