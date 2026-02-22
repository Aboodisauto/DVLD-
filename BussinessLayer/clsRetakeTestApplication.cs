using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class clsRetakeTestApplication : clsApplication
    {
        // First we make an application for the test retake
        // Then after we make the application we make a test appointment
        // The test appointment shuld have the local driving license application ID
        // Then in the retake application we add the retake test fee
        // and we book an new appointment and lock the other one
        public clsRetakeTestApplication() : base()
        {
            ApplicationID = -1;
            ApplicantID = -1;

            ApplicationDate = DateTime.Now;
            ApplicationType = 8;
            ApplicationStatus = 1;
            StatusDate = DateTime.Now;
            CreatedByUserID = -1;
            PaidFees = 5;
        }
        public bool AddApplication()
        {
            ApplicationID = clsApplicationsDAC.AddApplication(ApplicantID, ApplicationDate, ApplicationType, ApplicationStatus, StatusDate, PaidFees, CreatedByUserID);
            return ApplicationID != -1;
        }
        public static clsRetakeTestApplication Find(int ApplicationID)
        {
            clsApplication App = clsApplication.Find(ApplicationID);
            if(App == null)
            {
                return null;
            }
            return null;
        }
    }
}
