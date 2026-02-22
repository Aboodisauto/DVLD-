using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 4, LostReplacement = 3 };

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public int IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        // Wrapper for easier IssueReason text (Optional)
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        public class clsDetain
        {
            public int DetainID { get; set; }
            public decimal FineFees { get; set; }
            public DateTime DetainDate { get; set; }
            public clsDetain(int DetainID, decimal FineFees, DateTime DetainDate)
            {
                this.DetainID = DetainID;
                this.FineFees = FineFees;
                this.DetainDate = DetainDate;
            }
        }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = true;
            this.IssueReason = 1;
            this.CreatedByUserID = -1;
            this.Mode = enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.Mode = enMode.Update;
        }

        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseDAC.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseDAC.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
                this.IsActive, this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1;
            int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now;
            DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = true;
            int IssueReason = 1;
            int CreatedByUserID = -1;

            if (clsLicenseDAC.Find(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseDAC.GetAllLicenses();
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseDAC.GetDriverLicenses(DriverID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }

        public static bool Delete(int LicenseID)
        {
            return clsLicenseDAC.Delete(LicenseID);
        }

        public static string GetIssueReasonText(int IssueReason)
        {
            switch (IssueReason)
            {
                case 1:
                    return "FirstTime";
                case 2:
                    return "Renew";
                case 4:
                    return "Replacement for Damaged";
                case 3:
                    return "Replacement for Lost";
                default:
                    return "FirstTime";
            }
        }
        public static bool DoesPersonAlreadyHasALicense(int PersonID, int LicenseClassID)
        {
            return clsLicenseDAC.DoesPersonAlreadyHasALicense(PersonID,LicenseClassID);
        }
        public static int GetLicenseID(int ApplicationID)
        {
            return clsLicenseDAC.GetLicenseID(ApplicationID);
        }
        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int LicenseID = GetLicenseID(ApplicationID);
            return Find(LicenseID);
        }
        public static bool isLicenseDetained(int LicenseID)
        {
            return clsLicenseDAC.isLicenseDetained(LicenseID);
        }
        public static int DetainLicense(int LicenseID, decimal FineFees, int UserID)
        {
            return clsLicenseDAC.DetainLicense(LicenseID, FineFees, UserID);
        }
        public static bool ReleaseLicense(int LicenseID, int UserID, int ReleaseApplicationID)
        {
            return clsLicenseDAC.ReleaseDetainedLicense(LicenseID, UserID, ReleaseApplicationID);
        }
        public static DataTable FetchLicensesForPerson(int DriverID)
        {
            return clsLicenseDAC.FetchLicenseForPerson(DriverID);
        }
        public static clsDetain GetDetainInfoByLicenseID(int LicenseID)
        {
            int DetainID = -1;
            decimal FineFees = 0;
            DateTime DetainDate = DateTime.Now;
            if (clsLicenseDAC.GetDetainInfoByLicenseID(LicenseID, ref FineFees, ref DetainDate, ref DetainID))
            {
                return new clsDetain(DetainID, FineFees, DetainDate);
            }
            return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsLicenseDAC.GetAllDetainedLicenses();
        }
    }
}