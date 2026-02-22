using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsTest
    {
        enum enMode { Add , Update}
        enMode Mode;
        public int testID { set; get; }
        public bool testResult { set; get; }
        public int testAppointmentID { set; get; }
        public string Notes { set; get; }
        public int UserID { set; get; }

        public clsTest()
        {
            testID = -1;
            testResult = false;
            testAppointmentID = -1;
            Notes = string.Empty;
            UserID = -1;
            Mode = enMode.Add;
        }
        private clsTest(int testID, bool testResult, int testAppointmentID, string notes, int userID)
        {
            this.testID = testID;
            this.testResult = testResult;
            this.testAppointmentID = testAppointmentID;
            Notes = notes;
            UserID = userID;
            Mode = enMode.Update;
        }
        public static clsTest Find(int testAppointmentID)
        {
            int testID = -1, userID = -1;
            bool testResult = false;
            string notes = string.Empty;
            if (clsTestDAC.Find(testAppointmentID, ref testResult, ref testID, ref notes, ref userID)){
                return new clsTest(testID, testResult, testAppointmentID, notes, userID);
            }
            return null;
        }
        private bool _Add()
        {
            this.testID = clsTestDAC.Add(this.testResult,this.testAppointmentID,this.Notes,this.UserID);
            return testID != -1;
        }
        private bool _Update()
        {
            return clsTestDAC.Update(testID, this.testResult, this.testAppointmentID, this.Notes, this.UserID);
        }
        public bool Save()
        {
            bool isSaved = false;
            switch (Mode)
            {
                case enMode.Add:
                    isSaved = _Add();
                    break;
                case enMode.Update:
                    isSaved = _Update();
                    break;
            }
            return isSaved;
        }
        public static bool Delete(int testID)
        {
            return clsTestDAC.Delete(testID);
        }


        public static short CountPassedTests(int LocalLicenseApplicationID)
        {
            return clsTestDAC.CountPassedTests(LocalLicenseApplicationID);
        }
        public static bool isEligibleToTakeTest(int PersonID,int TestTypeID,int LicenseClassID)
        {
            return clsTestDAC.isEligibleToTakeTest(PersonID, TestTypeID,LicenseClassID);
        }
        public static short CalCulateTestTrail(int LocalApplicationID, int TestTypeID)
        {
            return clsTestDAC.CountOfFailed(LocalApplicationID, TestTypeID);
        }
    }
}
