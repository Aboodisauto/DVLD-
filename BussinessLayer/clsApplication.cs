using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public  class clsApplication
    {
        protected enum enMode {Add,Edit }
        protected enMode Mode = enMode.Add;
        public int ApplicationID { get; set; }
        public int ApplicantID { get; set; }
        public DateTime ApplicationDate { get; set;}
        public int ApplicationType { get; set; }
        public int ApplicationStatus { get; set; }
        public DateTime StatusDate { get; set; }
        public double PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        
       
        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantID = -1;

            ApplicationDate = DateTime.Now;
            ApplicationType = -1;
            ApplicationStatus = -1;
            StatusDate = DateTime.Now;
            CreatedByUserID = -1;
            PaidFees = -1;
            Mode = enMode.Add;
        }
        protected clsApplication(
            int ApplicationID,
            int ApplicantID,
            DateTime ApplicationDate,
            int ApplicationType,
            int ApplicationStatus,
            DateTime StatusDate,
            double PaidFees,
            int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantID = ApplicantID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationType = ApplicationType;
            this.ApplicationStatus = ApplicationStatus;
            this.StatusDate = StatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            Mode = enMode.Edit;
        }
        public static bool IsThereaDuplicate(int ApplicantID, int LicenseID)
        {
            return clsApplicationsDAC.IsThereaDuplicate(ApplicantID, LicenseID);
        }
        public static DataTable FetchLocalApplications()
        {
            return clsApplicationsDAC.FetchLocalApplications();
        }
        private bool _AddApplication()
        {
            this.ApplicationID = clsApplicationsDAC.AddApplication(ApplicantID, ApplicationDate, ApplicationType, ApplicationStatus, StatusDate, PaidFees, CreatedByUserID);
            return ApplicationID != -1;
        }
        private bool _UpdateApplication()
        {
            return clsApplicationsDAC.UpdateApplication(ApplicationID, ApplicantID, ApplicationDate, ApplicationType, ApplicationStatus, StatusDate, PaidFees, CreatedByUserID);
        }
        public static int GetApplicationID(int LocalApplicationID) 
        {
            return clsApplicationsDAC.GetApplicationID(LocalApplicationID);
        }
        public bool Save()
        {
            bool Done = false;
            switch (Mode)
            {
                case enMode.Add:
                    Done = _AddApplication(); Mode = enMode.Edit; break;
                case enMode.Edit:
                    Done = _UpdateApplication(); break;
            }
            return Done;
        }
        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantID = -1, ApplicationType = -1, ApplicationStatus = -1, CreatedByUserID = -1;
            double PaidFees = -1;
            DateTime ApplicationDate = DateTime.Now, StatusDate = DateTime.Now;
            if (clsApplicationsDAC.FindApplication(ApplicationID,ref ApplicantID, ref ApplicationDate,ref ApplicationType,ref ApplicationStatus,ref StatusDate,ref PaidFees,ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID,ApplicantID,ApplicationDate,ApplicationType,ApplicationStatus,StatusDate,PaidFees,CreatedByUserID);
            }
            return null;
        }
        public static bool Delete(int ApplicationID)
        {
            return clsApplicationsDAC.DeleteApplication(ApplicationID);
        }
        public static bool Cancel(int ApplicationID)
        {
            return clsApplicationsDAC.CancelApplication(ApplicationID);
        }
        public static string GetApplicantFullName(int ApplicantID)
        {
            return clsApplicationsDAC.GetApplicantFullName(ApplicantID);
        }
        

    }
}
