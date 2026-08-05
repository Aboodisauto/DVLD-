using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsTestAppointment
    {
        enum enMode { Add = 1, Update = 2 }
        private enMode Mode = enMode.Add;
        public int TestAppointmentID { set; get; }
        public int TestTypeID { set; get; }
        public int LocalApplicationID { set; get; }
        public clsLocalApplication LocalApplication { set; get; }
        public DateTime AppointmentDate { set; get; }
        public double TestPaidFees { set; get; }
        public int TestCreatedUser { set; get; }
        public bool isLocked { set; get; }
        // New Property Added
        public int RetakeApplicationID { set; get; }
        public clsApplication RetakeApplication { set; get; }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalApplicationID = -1;
            AppointmentDate = DateTime.Now;
            TestPaidFees = 0;
            TestCreatedUser = -1;
            isLocked = false;
            RetakeApplicationID = -1; // Initialize new property
            Mode = enMode.Add;
        }

        // Updated private constructor to include RetakeApplicationID
        private clsTestAppointment(int TestAppointmentID, int TestTypeID, int LocalApplicationID,
            DateTime AppointmentDate, double TestPaidFees, int CreatedByUser, bool isLocked, int RetakeApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalApplicationID = LocalApplicationID;
            this.LocalApplication = clsLocalApplication.Find(LocalApplicationID);
            this.AppointmentDate = AppointmentDate;
            this.TestPaidFees = TestPaidFees;
            this.TestCreatedUser = CreatedByUser;
            this.isLocked = isLocked;
            this.RetakeApplicationID = RetakeApplicationID; // Assign new property
            this.RetakeApplication = clsApplication.Find(RetakeApplicationID);
            Mode = enMode.Update;
        }

        private bool _AddAppointment()
        { 

            // Added RetakeApplicationID to the DAC call
            this.TestAppointmentID = clsTestAppointmentDAC.AddAppointment(this.TestTypeID, this.LocalApplicationID,
                this.TestCreatedUser, this.TestPaidFees, this.isLocked, this.AppointmentDate, this.RetakeApplicationID);

            return (TestAppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            // Added RetakeApplicationID to the DAC call
            return clsTestAppointmentDAC.Update(this.TestAppointmentID, this.TestTypeID, this.LocalApplicationID,
                this.TestCreatedUser, this.TestPaidFees, this.isLocked, this.AppointmentDate, this.RetakeApplicationID);
        }

        public static bool DeleteAppointment(int TestAppointmentID)
        {
            return clsTestAppointmentDAC.Delete(TestAppointmentID);
        }

        public bool Save()
        {
            bool isSaved = false;
            switch (Mode)
            {
                case enMode.Add:
                    isSaved = _AddAppointment();
                    break;
                case enMode.Update:
                    isSaved = _UpdateAppointment();
                    break;
            }
            return isSaved;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalApplicationID = -1, CreatedByUserID = -1;
            int RetakeApplicationID = -1; // Variable for new property
            double PaidFees = 0;
            bool isLocked = false;
            DateTime AppointmentDate = DateTime.Now;

            // Added ref RetakeApplicationID to the DAC Find call
            if (clsTestAppointmentDAC.Find(TestAppointmentID, ref TestTypeID, ref LocalApplicationID,
                ref CreatedByUserID, ref PaidFees, ref isLocked, ref AppointmentDate, ref RetakeApplicationID))
            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalApplicationID,
                    AppointmentDate, PaidFees, CreatedByUserID, isLocked, RetakeApplicationID);
            }
            return null;
        }

        public static DataTable FetchTestAppointments(int LocalApplicationID, int TestTypeID)
        {
            return clsTestAppointmentDAC.FetchTestAppointments(LocalApplicationID, TestTypeID);
        }
    }
}