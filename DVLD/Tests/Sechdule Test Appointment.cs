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
    public partial class Sechdule_Test_Appointment : Form
    {
        int lApplicationID;
        enum enTestType { enVision = 1, enWritten = 2, enStreet = 3}
        enTestType eTestType;
        clsLocalApplication LApplication;
        private void _RefreshAppointmentDates()
        {
            dataGridView1.DataSource = clsTestAppointment.FetchTestAppointments(lApplicationID,(int)eTestType);
        }
        private void _ChangeTheFormAccordingToTestType(int TestType)
        {
            switch (TestType)
            {
                case 1:
                    eTestType = enTestType.enVision;
                    TestName.Text = "Vision";
                    pictureBox1.Image = Resources.Vision_512;
                    break;
                case 2:
                    eTestType = enTestType.enWritten;
                    TestName.Text = "Written";
                    pictureBox1.Image = Resources.Written_Test_512;
                    break;
                case 3:
                    eTestType = enTestType.enStreet;
                    TestName.Text = "Street";
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
            }
        }
        public Sechdule_Test_Appointment(int LocalApplicationID, int TestType)
        {
            InitializeComponent();
            lApplicationID = LocalApplicationID;
            LApplication = clsLocalApplication.Find(LocalApplicationID);
            applicationBasicInfo1.localApplication = LApplication;
            applicationBasicInfo1._LoadLocalApplicationData();
            driving_License_Application_Info1.localApplication = LApplication;
            driving_License_Application_Info1._RefreshLocalApplication();
            _ChangeTheFormAccordingToTestType(TestType);
            _RefreshAppointmentDates();
        }
        private bool _IsEligibleToTakeTest(int PersonID, int TestTypeID, int LicenseClassID)
        {
            return clsTest.isEligibleToTakeTest(PersonID, TestTypeID,LicenseClassID);
        }
        private void AddAppointment_Click(object sender, EventArgs e)
        {
            if(_IsEligibleToTakeTest(LApplication.ApplicantID,(int)eTestType,LApplication.LicenseClassID))
            {
                MessageBox.Show("This person already has an appointment or already passed the exam !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Form frm = new Tests.BookAnAppointment(-1,LApplication,(int)eTestType);
            frm.ShowDialog();
            _RefreshAppointmentDates();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            Form frm = new BookAnAppointment(ID, LApplication, (int)eTestType);
            frm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Tests.TakeTestForm((int)dataGridView1.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreshAppointmentDates();
            driving_License_Application_Info1._RefreshLocalApplication();
        }
    }
}
