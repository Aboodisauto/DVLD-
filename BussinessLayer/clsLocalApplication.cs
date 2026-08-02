using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BussinessLayer
{
    public class clsLocalApplication : clsApplication
    {
        public int LocalApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public string PersonFullName {  get { return base.ApplicantInfo.FullName;  }  }
        public clsLocalApplication() : base()
        {
            ApplicationType = 1;
            ApplicationStatus = 1;
            LocalApplicationID = -1;
            LicenseClassID = -1;
        }
        protected clsLocalApplication(int LocalLicenseID, int LicenseClassID, clsApplication App) : base(App.ApplicationID, App.ApplicantID, App.ApplicationDate, App.ApplicationType, App.ApplicationStatus, App.StatusDate, App.PaidFees, App.CreatedByUserID)
        {
            this.LocalApplicationID = LocalLicenseID;
            this.LicenseClassID = LicenseClassID;
        }
        private bool _AddNewLocalApplication()
        {
            if (!base.Save())
            {
                return false;
            }
            int LocalApplicationID = clsLocalApplicationDAC.AddLocalApplication(ApplicationID, LicenseClassID);
            return LocalApplicationID > 0;

        }
        private bool _UpdateLocalApplication()
        {
            if (!base.Save())
            {
                return false;
            }
            return clsLocalApplicationDAC.UpdateLocalApplication(LicenseClassID);
        }
        public static clsLocalApplication Find(int LocalApplicationID)
        {
            clsApplication application;
            int ApplicationID = -1, LicenseClassID = -1;
            if (!clsLocalApplicationDAC.FindLocalApplication(LocalApplicationID, ref LicenseClassID, ref ApplicationID))
            {
                return null;
            }
            application = clsApplication.Find(ApplicationID);
            if (application == null)
                return null;
            return new clsLocalApplication(LocalApplicationID, LicenseClassID, application);
        }
        public static bool Cancel(int LocalApplicationID)
        {
            int ApplicationID = clsApplicationsDAC.GetApplicationID(LocalApplicationID);
            return clsApplication.Cancel(ApplicationID);
        }
        public bool Save()
        {
            bool Done = false;
            switch (Mode)
            {
                case enMode.Add:
                    Done = _AddNewLocalApplication();
                    break;
                case enMode.Edit:
                    Done = _UpdateLocalApplication();
                    break;
            }
            return Done;
        }
        public static short GetLicenseClassID(string ClassName) { return clsLocalApplicationDAC.FetchLicenseClassID(ClassName); }

        public static bool Delete(int LocalApplicationID)
        {
            int ApplicationID = clsLocalApplicationDAC.GetApplicationID(LocalApplicationID);
            bool ApplicationDeleted = clsLocalApplicationDAC.Delete(LocalApplicationID);
            return (clsApplication.Delete(ApplicationID) && ApplicationDeleted);
        }
        public static clsLocalApplication FindByLicenseID(int LicenseID)
        {
            int localApplicationID = clsLocalApplicationDAC.GetLocalApplicationID(LicenseID);
            return Find(localApplicationID);
        }

        public static int GetLocalApplicationIDByLicenseID(int licenseID)
        {
            return clsLocalApplicationDAC.GetLocalApplicationID(licenseID);
        }
    }
}
